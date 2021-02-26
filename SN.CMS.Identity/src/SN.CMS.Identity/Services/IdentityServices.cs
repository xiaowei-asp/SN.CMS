using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SN.CMS.Common.Authentications;
using SN.CMS.Common.RabbitMq;
using SN.CMS.Identity.Domain;
using SN.CMS.Identity.Messages.Events;
using SN.CMS.Identity.Repostories;

namespace SN.CMS.Identity.Services
{
    public class IdentityServices : IIdentityServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtHandler _jwtHandler;
        private readonly ISNRabbitMqClient _snRabbitMqClient;
        private readonly IDistributedCache _distributedCache;

        public IdentityServices(IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IPasswordHasher<User> passwordHasher,
            IJwtHandler jwtHandler,
            ISNRabbitMqClient snRabbitMqClient,
            IDistributedCache distributedCache
        )
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _passwordHasher = passwordHasher;
            _jwtHandler = jwtHandler;
            _snRabbitMqClient = snRabbitMqClient;
            _distributedCache = distributedCache;
        }

        public async Task<string> GetAsync(string userId)
        {
            var value = await _distributedCache.GetAsync(userId);
            var userName = System.Text.Encoding.Default.GetString(value);
            return userName;
        }

        public async Task SignUpAsync(Guid id, string name, string password, string role = Role.User)
        {
            var user = await _userRepository.GetAsync(name);
            if (user != null)
            {
                throw new Exception($"用户名: '{name}' 已存在，请重新注册.");
            }

            user = new User(id, name, role);
            user.SetPassword(password, _passwordHasher);
            await _userRepository.AddAsync(user);

            //发布用户注册数据到队列
            _snRabbitMqClient.Publish(JsonConvert.SerializeObject(new SignedUp(id, name, role)));

            
        }

        public async Task<JsonWebToken> SignInAsync(string name, string password)
        {
            var user = await _userRepository.GetAsync(name);
            if (user == null || !user.ValidatePassword(password, _passwordHasher))
            {
                throw new Exception("用户名或密码错误.");
            }

            var refreshToken = new RefreshToken(user, _passwordHasher);
            var jwt = _jwtHandler.CreateToken(user.Id.ToString("N"), user.Role, 
                new Dictionary<string, string>()
                {
                    ["Company"] = "SN"
                });
            jwt.RefreshToken = refreshToken.Token;
            await _refreshTokenRepository.AddAsync(refreshToken);
            //将用户添加到redis
            var userValue = System.Text.Encoding.Default.GetBytes(user.Name);
            await _distributedCache.SetAsync(user.Id.ToString(), userValue);
            _snRabbitMqClient.Publish(JsonConvert.SerializeObject(new SignedIn(user.Id)));
            return jwt;
        }
    }
}

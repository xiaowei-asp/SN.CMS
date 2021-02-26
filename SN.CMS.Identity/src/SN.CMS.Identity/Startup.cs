using System.Reflection;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SN.CMS.Common.Authentications;
using SN.CMS.Common.Domain;
using SN.CMS.Common.ErrorHandler;
using SN.CMS.Common.RabbitMq;
using SN.CMS.Common.Redis;
using SN.CMS.Identity.Domain;

namespace SN.CMS.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddRedis();
            services.AddJwt();
            services.Configure<DbOptions>(Configuration.GetSection("sqlite"));
            services.AddRabbitMq();
            services.AddMvcCore()
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    o.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    o.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                    o.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    o.SerializerSettings.Formatting = Formatting.Indented;
                    o.SerializerSettings.Converters.Add(new StringEnumConverter());
                })
                .AddDataAnnotations()
                .AddApiExplorer();
            services.AddAuthorization();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors =>
                        cors.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });
            services.AddDbContext<SNCMSDbContext>();
        }


        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.RegisterType<PasswordHasher<User>>().As<IPasswordHasher<User>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

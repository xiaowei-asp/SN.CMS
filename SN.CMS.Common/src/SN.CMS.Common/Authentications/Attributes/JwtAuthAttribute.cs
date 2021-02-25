using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SN.CMS.Common.Authentications.Attributes
{
    public class JwtAuthAttribute : AuthAttribute
    {
        public JwtAuthAttribute(string policy = "") : base(JwtBearerDefaults.AuthenticationScheme, policy)
        {
        }
    }
}

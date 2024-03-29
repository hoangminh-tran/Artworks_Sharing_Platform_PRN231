using Artworks_Sharing_Plaform_Api.Enum;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Artworks_Sharing_Plaform_Api.Service.Interface;

namespace Artworks_Sharing_Plaform_Api.Service
{
    public class HelpperService : IHelpperService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _http;

        public HelpperService(IConfiguration configuration, IHttpContextAccessor http)
        {
            _configuration = configuration;
            _http = http;
        }

        public bool CheckBearerTokenIsValidAndNotExpired(string token)
        {
            var securityKey = _configuration.GetSection("AppSettings:Token").Value ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out SecurityToken validatedToken);
                // Check Token Is Expired
                if (validatedToken.ValidTo < DateTime.Now)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Guid GetAccIdFromLogged()
        {
            var AccId = _http.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;
            return AccId == null ? throw new Exception(ServerErrorEnum.SERVER_ERROR) : Guid.Parse(AccId);
        }

        public Guid GetAccIdFromLoogedNotThrow()
        {
            var AccId = _http.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;
            return AccId == null ? Guid.Empty : Guid.Parse(AccId);
        }

        public bool IsTokenValid()
        {
            var token = _http.HttpContext?.Request.Headers.Authorization.ToString().Replace("Bearer ", "");            
            if (token == null || !CheckBearerTokenIsValidAndNotExpired(token))
            {
                return false;
            }
            return true;
        }
    }
}

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationServer
{
    public class JwtAuthenticationManager
    {
        public const string JWT_TOKEN_KEY = "muthuviswanath@outlook.com";
        private const int JWT_TOKEN_VALIDITY = 25;
        public static AuthenticationResponse Authenticate(AuthenticationRequest authenticationRequest) {
            if (authenticationRequest.UserName != "admin" || authenticationRequest.Password != "admin") {
                return null;
            }

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(JWT_TOKEN_KEY);
            var tokenExpiryDateTime = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY);
            var securityTokenDescriptor = new SecurityTokenDescriptor {
                Subject = new System.Security.Claims.ClaimsIdentity(new List<Claim>
                {
                    new Claim("username", authenticationRequest.UserName),
                    new Claim(ClaimTypes.Role, "Administrator"),

                }),
                Expires = tokenExpiryDateTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                AccessToken = token,
                ExpiresIn = (int)tokenExpiryDateTime.Subtract(DateTime.Now).TotalSeconds

            };
            }
        }
    }


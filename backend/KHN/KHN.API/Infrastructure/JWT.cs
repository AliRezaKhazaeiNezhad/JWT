using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace KHN.API.Infrastructure
{
    public class JWT : IJWT
    {
        private readonly IConfiguration _configuration;

        public JWT(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(int expirePerminute, string usernmae, int role)
        {

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var secretKey = _configuration["Jwt:SecretKey"];


            var token = new JwtSecurityToken(
              issuer,
              audience,
              new Claim[] {
                  new Claim(ClaimTypes.Name, usernmae),
                  new Claim(ClaimTypes.Role, role.ToString())
              },
              expires: DateTime.UtcNow.AddMinutes(expirePerminute),
              signingCredentials:
              new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256)
          );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }


        public bool ValidateToken(string token)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var secretKey = _configuration["Jwt:SecretKey"];


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = TokenValidationParameters();

            try
            {
                tokenHandler.ValidateToken(
                    token, 
                    tokenValidationParameters, 
                    out SecurityToken validatedToken);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        private TokenValidationParameters TokenValidationParameters()
        {

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var secretKey = _configuration["Jwt:SecretKey"];


            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };


            return tokenValidationParameters;
        }
    }
}

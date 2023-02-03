using ExercicioSemana2.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExercicioSemana2.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GerarToken(Funcionario funcionaro)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var issuer = _config.GetValue<dynamic>("Jwt:Issuer");
            var audience = _config.GetValue<dynamic>("Jwt:Audience");
            var KeyJwt = _config.GetValue<dynamic>("Jwt:Key");
            var key = Encoding.ASCII.GetBytes(KeyJwt);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, funcionaro.Nome),
                new Claim(JwtRegisteredClaimNames.Email, funcionaro.Email),
                new Claim(ClaimTypes.Role, funcionaro.Permissao.Nome),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

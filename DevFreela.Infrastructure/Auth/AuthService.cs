using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevFreela.Infrastructure.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(string email, string role)
        {
            // Emissor do serviço do token
            var issuer = _configuration["Jwt:Issuer"] ?? string.Empty;
            // Publico do serviço do token
            var audience = _configuration["Jwt:Audience"] ?? string.Empty;
            // A Chave do serviço do token 
            var key = _configuration["Jwt:Key"] ?? string.Empty;

            #region Chave/Criptografia
            // Obtendo a chave e convertendo em formato simétrico
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            // Obtendo a assinatura da chave simetrica criada no método acima, adicionando a criptografia 'HmacSha256',
            // Criando a credencial de assinatura
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            #endregion

            #region Claims de usuário
            var claims = new List<Claim>
            {
                new("userName", email),
                new(ClaimTypes.Role, role)
            };
            #endregion

            #region Configuração/Geração do Token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials,
                claims: claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);

            #endregion

            return stringToken;
        }
    }
}

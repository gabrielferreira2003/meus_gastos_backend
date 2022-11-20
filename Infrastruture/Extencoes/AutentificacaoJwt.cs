using Domain.Entidades;
using meus_gastos_backend;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class AutentificacaoJwt
    {
        public static async Task<string> GerarJwt(string email, 
            UserManager<ApplicationUser> userManager, 
            AppSettings appSettings)
        {
            var user = await userManager.FindByEmailAsync(email);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = appSettings.Emissor,
                Audience = appSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

    }
}

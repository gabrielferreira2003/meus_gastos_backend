using MeusGastos.Domain.Entidades;
using MeusGastos;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
namespace MeusGastos.Infrastructure.Extensions
{
    public static class AutentificacaoJwt
    {
        public static async Task<string> GerarJwt(string email,
            UserManager<ApplicationUser> userManager,
            AppSettings appSettings)
        {
            var user = await userManager.FindByEmailAsync(email);

            var userClaims = await userManager.GetClaimsAsync(user);
            userClaims.Add(new Claim("Token", "Access"));

            var claims = new ClaimsIdentity(
                   identity: new DetalhesAutenticacaoDTO
                   {
                       IsAuthenticated = true,
                       Name = user.Id.ToString()
                   },

                   claims: userClaims);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Issuer = appSettings.Emissor,
                Audience = appSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
    public class DetalhesAutenticacaoDTO : IIdentity
    {
        public string? AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string? Name { get; set; }
    }
}
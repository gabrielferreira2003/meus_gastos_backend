using System.Threading;
using MeusGastos.Domain.Entidades;
using MeusGastos.Infrastructure.Extensions;
using MediatR;
using MeusGastos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace MeusGastos.Application.Features.User.Command.UserLogin
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, UserLoginCommandResponse>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppSettings _appSettings;
        public UserLoginCommandHandler(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public async Task<UserLoginCommandResponse> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, true);

                if (!result.Succeeded) throw new Exception("Usuário ou senha inválidos");

                return new UserLoginCommandResponse()
                {
                    Token = await AutentificacaoJwt.GerarJwt(request.Email, _userManager, _appSettings)
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

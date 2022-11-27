using MediatR;
using Microsoft.AspNetCore.Identity;
using MeusGastos;
using Microsoft.Extensions.Options;
using MeusGastos.Infrastructure.Extensions;
using MeusGastos.Domain.Entidades;
using MeusGastos.Application.Features.Finance.Command.AddFinance;
using MeusGastos.Infrastructure.Contexto;
using Microsoft.EntityFrameworkCore;

namespace MeusGastos.Application.Features.User.Command.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserCommandResponse>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly IMediator _mediator;
        private readonly Contexto _contexto;

        public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<AppSettings> appSettings,
            IMediator mediator,
            Contexto contexto)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = request.Email,
                    Email = request.Email,
                    EmailConfirmed = true,
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded) throw new Exception("" + result.Errors);

                await _signInManager.SignInAsync(user, false);

                var usuarioCriado = await _contexto.ApplicationUser.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                if (usuarioCriado == null) { throw new Exception("Usuario não encontrado!"); }

                var financa = new AddFinanceCommand()
                {
                    Patrimonio = request.CapitalInicial,
                    UsuarioId = usuarioCriado.Id
                };

                await _mediator.Send(financa);

                return new RegisterUserCommandResponse()
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

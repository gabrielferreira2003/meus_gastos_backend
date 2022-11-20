using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using meus_gastos_backend;
using Microsoft.Extensions.Options;
using Infrastructure.Extensions;
using Domain.Entidades;
using Application.Finance.Command.AddFinance;
using Infrastruture.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Application.User.Command.RegisterUser
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
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _mediator = mediator;
            _contexto = contexto;
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

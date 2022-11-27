using MediatR;
using MeusGastos.Infrastructure.Contexto;
using MeusGastos.Domain.Entidades;
using MeusGastos.Application.Services.UserService;
using MeusGastos.Application.Features.Finance.Command.AddValueFinance;
using Microsoft.EntityFrameworkCore;

namespace MeusGastos.Application.Features.Gains.Command.AddGains
{
    public class AddGainsCommandHandler : IRequestHandler<AddGainsCommand, AddGainsCommandResponse>
    {
        private readonly Contexto _contexto;
        private readonly IMediator _mediator;
        private readonly IUserService _userService;
        public AddGainsCommandHandler(Contexto contexto, 
            IMediator mediator,
            IUserService userService)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<AddGainsCommandResponse> Handle(AddGainsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var usuarioSelecionado = await _contexto.ApplicationUser.FirstOrDefaultAsync(u => u.Id == _userService.GetUserId(), cancellationToken);

                var ganhosAdicionar = new Ganhos()
                { 
                    Valor = request.Valor,
                    Descricao = request.Descricao,
                    Hora = DateTime.UtcNow,
                    FinancasId = usuarioSelecionado!.Financas.First().Id,
                };

                _contexto.Ganhos.Add(ganhosAdicionar);
                await _contexto.SaveChangesAsync(cancellationToken);

                var parametroSomarGanhos = new AddValueFinanceCommand()
                {
                    Id = usuarioSelecionado!.Financas.First().Id,
                    Valor = request.Valor,
                };

                var resposta = await _mediator.Send(parametroSomarGanhos, cancellationToken);

                return new AddGainsCommandResponse() { Sucesso = true };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

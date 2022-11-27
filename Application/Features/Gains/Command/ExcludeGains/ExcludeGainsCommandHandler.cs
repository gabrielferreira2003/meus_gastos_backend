using MediatR;
using MeusGastos.Infrastructure.Contexto;
using MeusGastos.Application.Services.UserService;
using Microsoft.EntityFrameworkCore;
using MeusGastos.Application.Features.Finance.Command.SubtractExpensesFinance;
using MeusGastos.Application.Features.Finance.Command.AddValueFinance;

namespace MeusGastos.Application.Features.Gains.Command.ExcludeGains
{
    public class ExcludeGainsCommandHandler : IRequestHandler<ExcludeGainsCommand, ExcludeGainsCommandResponse>
    {
        private readonly Contexto _contexto;
        private readonly IMediator _mediator;
        private readonly IUserService _userService;
        public ExcludeGainsCommandHandler(Contexto contexto, 
            IMediator mediator,
            IUserService userService)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<ExcludeGainsCommandResponse> Handle(ExcludeGainsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var usuarioSelecionado = await _contexto.ApplicationUser.FirstOrDefaultAsync(u => u.Id == _userService.GetUserId(), cancellationToken);

                var ganhoSelecionado = await _contexto.Ganhos.FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
                if (ganhoSelecionado == null) { throw new Exception("ERRO"); }

                var parametroSomarGanhos = new AddValueFinanceCommand()
                {
                    Id = usuarioSelecionado!.Financas.First().Id,
                    Valor = - ganhoSelecionado.Valor
                };

                var resposta = await _mediator.Send(parametroSomarGanhos, cancellationToken);

                _contexto.Ganhos.Remove(ganhoSelecionado);
                await _contexto.SaveChangesAsync(cancellationToken);

                return new ExcludeGainsCommandResponse() { Sucesso = true };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

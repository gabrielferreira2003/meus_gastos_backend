using MediatR;
using Infrastruture.Contexto;
using Application.Finance.Command.ExcludeGainsFinance;
using Microsoft.EntityFrameworkCore;

namespace Application.Gains.Command.ExcludeGains
{
    public class ExcludeGainsCommandHandler : IRequestHandler<ExcludeGainsCommand, ExcludeGainsCommandResponse>
    {
        private readonly Contexto _contexto;
        private readonly IMediator _mediator;
        public ExcludeGainsCommandHandler(Contexto contexto, IMediator mediator)
        {
            _contexto = contexto;
            _mediator = mediator;
        }

        public async Task<ExcludeGainsCommandResponse> Handle(ExcludeGainsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ganhoSelecionado = _contexto.Ganhos.FirstOrDefault(g => g.Id == request.Id);
                if (ganhoSelecionado == null) { throw new Exception("ERRO"); }

                var parametroSomarGanhos = new ExcludeGainsFinanceCommand()
                {
                    Id = request.FinancaId,
                    Ganhos = ganhoSelecionado.Valor,
                    Patrimonio = ganhoSelecionado.Financas.Patrimonio,
                    UsuarioId = ganhoSelecionado.Financas.UsuarioId,
                };

                var resposta = await _mediator.Send(parametroSomarGanhos);

                _contexto.Ganhos.Remove(ganhoSelecionado);
                await _contexto.SaveChangesAsync();

                return new ExcludeGainsCommandResponse() { Sucesso = true };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using MediatR;
using MeusGastos.Infrastructure.Contexto;
using Microsoft.EntityFrameworkCore;
using MeusGastos.Domain.Entidades;

namespace MeusGastos.Application.Features.Finance.Command.SubtractExpensesFinance
{
    public class SubtractExpensesFinanceCommandHandler : IRequestHandler<SubtractExpensesFinanceCommand, SubtractExpensesFinanceCommandResponse>
    {
        private readonly Contexto _contexto;
        public SubtractExpensesFinanceCommandHandler(Contexto contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public async Task<SubtractExpensesFinanceCommandResponse> Handle(SubtractExpensesFinanceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var financaSelecionada = await _contexto.Financas.FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

                financaSelecionada!.Patrimonio = financaSelecionada.Patrimonio - request.Gastos;

                _contexto.Financas.Update(financaSelecionada);
                await _contexto.SaveChangesAsync(cancellationToken);

                return new SubtractExpensesFinanceCommandResponse() { Sucesso = true };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

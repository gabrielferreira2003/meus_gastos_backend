using MediatR;
using MeusGastos.Infrastructure.Contexto;
using Microsoft.EntityFrameworkCore;

namespace MeusGastos.Application.Features.Finance.Command.AddValueFinance
{
    public class AddValueFinanceCommandHandler : IRequestHandler<AddValueFinanceCommand, AddValueFinanceCommandResponse>
    {
        private readonly Contexto _contexto;
        public AddValueFinanceCommandHandler(Contexto contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public async Task<AddValueFinanceCommandResponse> Handle(AddValueFinanceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var financaSelecionada = await _contexto.Financas.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                financaSelecionada!.Patrimonio = financaSelecionada.Patrimonio + request.Valor;

                _contexto.Financas.Update(financaSelecionada);
                await _contexto.SaveChangesAsync(cancellationToken);

                return new AddValueFinanceCommandResponse() { Sucesso = true };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

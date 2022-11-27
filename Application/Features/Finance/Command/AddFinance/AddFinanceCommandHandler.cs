using MediatR;
using MeusGastos.Infrastructure.Contexto;
using MeusGastos.Domain.Entidades;

namespace MeusGastos.Application.Features.Finance.Command.AddFinance
{
    public class AddFinanceCommandHandler : IRequestHandler<AddFinanceCommand, AddFinanceCommandResponse>
    {
        private readonly Contexto _contexto;
        public AddFinanceCommandHandler(Contexto contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public async Task<AddFinanceCommandResponse> Handle(AddFinanceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var financasAdicionar = new Financas()
                {
                    Patrimonio = request.Patrimonio,
                    UsuarioId = request.UsuarioId,
                };

                _contexto.Financas.Add(financasAdicionar);
                await _contexto.SaveChangesAsync();

                return new AddFinanceCommandResponse() { Sucesso = true };
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

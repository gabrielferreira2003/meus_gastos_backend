using MediatR;
using MeusGastos.Infrastructure.Contexto;
using MeusGastos.Infrastructure.Extencoes;

namespace MeusGastos.Application.Features.Finance.Query.SearchFinance
{
    public class GetFinanceQueryHandler : IRequestHandler<GetFinanceQuery, GetFinanceQueryResponse>
    {
        private readonly Contexto _contexto;
        public GetFinanceQueryHandler(Contexto contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public async Task<GetFinanceQueryResponse> Handle(GetFinanceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var financaSelecionada = _contexto.Financas.FirstOrDefault(f => f.UsuarioId == request.UsuarioId);
                if (financaSelecionada == null) throw new Exception("Finança não encontrada!");

                double somaGanhos = SomarArray.Ganhos(financaSelecionada.Ganhos);
                double somaGastos = SomarArray.Gastos(financaSelecionada.Gastos);

                var financaRetorno = new GetFinanceQueryResponse()
                {
                    Id = financaSelecionada.Id,
                    Patrimonio = financaSelecionada.Patrimonio,
                    Ganhos = somaGanhos,
                    Gastos = somaGastos,
                };

                return financaRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

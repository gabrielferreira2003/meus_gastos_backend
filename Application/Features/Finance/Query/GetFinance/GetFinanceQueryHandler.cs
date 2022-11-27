using MediatR;
using MeusGastos.Application.Services.UserService;
using MeusGastos.Infrastructure.Contexto;
using Microsoft.EntityFrameworkCore;

namespace MeusGastos.Application.Features.Finance.Query.SearchFinance
{
    public class GetFinanceQueryHandler : IRequestHandler<GetFinanceQuery, GetFinanceQueryResponse>
    {
        private readonly Contexto _contexto;
        private readonly IUserService _userService;
        public GetFinanceQueryHandler(Contexto contexto,
            IUserService userService)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<GetFinanceQueryResponse> Handle(GetFinanceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var financaSelecionada = await _contexto.Financas.FirstOrDefaultAsync(f => f.UsuarioId == _userService.GetUserId(), cancellationToken);
                if (financaSelecionada == null) throw new Exception("Finança não encontrada!");

                var somaGanhos = financaSelecionada.Ganhos.Sum(g => g.Valor);
                var somaGastos = financaSelecionada.Gastos.Sum(g => g.Valor);

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

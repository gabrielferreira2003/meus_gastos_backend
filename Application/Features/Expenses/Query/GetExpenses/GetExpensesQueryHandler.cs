using MediatR;
using MeusGastos.Infrastructure.Contexto;
using Microsoft.EntityFrameworkCore;
using MeusGastos.Application.Services.UserService;

namespace MeusGastos.Application.Features.Expenses.Query.GetExpenses
{
    public class GetExpensesQueryHandler : IRequestHandler<GetExpensesQuery, IEnumerable<GetExpensesQueryResponse>>
    {
        private readonly Contexto _contexto;
        private readonly IUserService _userService;
        public GetExpensesQueryHandler(Contexto contexto,
            IUserService userService)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<IEnumerable<GetExpensesQueryResponse>> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var usuarioSelecionado = await _contexto.ApplicationUser.FirstOrDefaultAsync(u => u.Id == _userService.GetUserId());
                
                return await _contexto.Gastos
                    .Where(g => g.FinancasId == usuarioSelecionado.Financas.First().Id)
                    .Select(gasto => new GetExpensesQueryResponse
                    {
                        Id = gasto.Id,
                        Valor = gasto.Valor,
                        Descricao = gasto.Descricao,
                        Hora = gasto.Hora,
                    })
                    .OrderByDescending(g => g.Hora)
                    .ToListAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

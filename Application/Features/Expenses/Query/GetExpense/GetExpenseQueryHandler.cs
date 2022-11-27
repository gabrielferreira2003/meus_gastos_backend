using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeusGastos.Infrastructure.Contexto;
using Microsoft.EntityFrameworkCore;

namespace MeusGastos.Application.Features.Expenses.Query.GetExpense
{
    public class GetExpenseQueryHandler : IRequestHandler<GetExpenseQuery, GetExpenseQueryResponse>
    {
        private readonly Contexto _contexto;
        public GetExpenseQueryHandler(Contexto contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public async Task<GetExpenseQueryResponse> Handle(GetExpenseQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var gastoSelecionado = await _contexto.Gastos.FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

                return new GetExpenseQueryResponse()
                {
                    Valor = gastoSelecionado.Valor,
                    Descricao = gastoSelecionado.Descricao,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

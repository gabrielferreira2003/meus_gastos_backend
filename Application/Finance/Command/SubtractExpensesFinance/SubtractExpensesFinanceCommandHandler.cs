using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastruture.Contexto;
using Microsoft.EntityFrameworkCore;
using Domain.Entidades;

namespace Application.Finance.Command.SubtractExpensesFinance
{
    public class SubtractExpensesFinanceCommandHandler : IRequestHandler<SubtrairGastosCommand, SubtractExpensesFinanceCommandResponse>
    {
        private readonly Contexto _contexto;
        public SubtractExpensesFinanceCommandHandler(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<SubtractExpensesFinanceCommandResponse> Handle(SubtrairGastosCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var financaSelecionada = _contexto.Financas.AsNoTracking().FirstOrDefault(f => f.Id == request.Id);

                var financaAtualizada = new Financas()
                {
                    Id = financaSelecionada.Id,
                    Patrimonio = financaSelecionada.Patrimonio - request.Gastos,
                    Ganhos = null,
                    Gastos = null,
                };

                _contexto.Financas.Update(financaAtualizada);
                await _contexto.SaveChangesAsync();

                return new SubtractExpensesFinanceCommandResponse() { Sucesso = true };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

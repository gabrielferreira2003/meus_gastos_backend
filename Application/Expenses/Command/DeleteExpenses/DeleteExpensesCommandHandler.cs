using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastruture.Contexto;

namespace Application.Expenses.Command.DeleteExpenses
{
    public class DeleteExpensesCommandHandler : IRequestHandler<DeleteExpensesCommand, DeleteExpensesCommandResponse>
    {
        private readonly Contexto _contexto;
        public DeleteExpensesCommandHandler(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<DeleteExpensesCommandResponse> Handle(DeleteExpensesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var gastoSelecionado = _contexto.Gastos.FirstOrDefault(g => g.Id == request.Id);
                if (gastoSelecionado == null) { new Exception("Gasto não encontrado!"); }

                _contexto.Gastos.Remove(gastoSelecionado);
                await _contexto.SaveChangesAsync();

                return new DeleteExpensesCommandResponse() { Sucesso = true };
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}

using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastruture.Contexto;
using Application.Finance.Command.SubtractExpensesFinance;
using Domain.Entidades;

namespace Application.Expenses.Command.AddExpenses
{
    public class AddExpensesCommandHandler : IRequestHandler<AddExpensesCommand, AddExpensesCommandResponse>
    {
        private readonly Contexto _contexto;
        private readonly IMediator _mediator;
        public AddExpensesCommandHandler(Contexto contexto, IMediator mediator)
        {
            _contexto = contexto;
            _mediator = mediator;
        }

        public async Task<AddExpensesCommandResponse> Handle(AddExpensesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var gastosAdicionar = new Gastos()
                { 
                    Valor = request.Valor,
                    Descricao = request.Descricao,
                    Hora = DateTime.UtcNow,
                    FinancasId = request.FinancasId,
                };

                _contexto.Gastos.Add(gastosAdicionar);
                await _contexto.SaveChangesAsync();

                SubtrairGastosCommand parametroSubtrairGastos = new SubtrairGastosCommand()
                {
                    Id = request.FinancasId,
                    Gastos = request.Valor,
                };

                await _mediator.Send(parametroSubtrairGastos);

                return new AddExpensesCommandResponse() { Sucesso = true };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Application.Features.Expenses.Command.EditExpenses
{
    public class EditExpensesCommand : IRequest<EditExpensesCommandResponse>
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
    }
}

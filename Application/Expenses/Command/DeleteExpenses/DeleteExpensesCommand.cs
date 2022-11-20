using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Expenses.Command.DeleteExpenses
{
    public class DeleteExpensesCommand : IRequest<DeleteExpensesCommandResponse>
    {
        public int Id { get; set; }
    }
}

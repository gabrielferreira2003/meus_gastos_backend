using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Finance.Command.SubtractExpensesFinance
{
    public class SubtrairGastosCommand : IRequest<SubtractExpensesFinanceCommandResponse>
    {
        public int Id { get; set; }
        public double Gastos { get; set; }
    }
}

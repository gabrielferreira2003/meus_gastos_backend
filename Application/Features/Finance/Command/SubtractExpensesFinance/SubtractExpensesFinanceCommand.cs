using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Application.Features.Finance.Command.SubtractExpensesFinance
{
    public class SubtractExpensesFinanceCommand : IRequest<SubtractExpensesFinanceCommandResponse>
    {
        public int Id { get; set; }
        public double Gastos { get; set; }
    }
}

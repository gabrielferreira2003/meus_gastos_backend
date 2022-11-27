using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Application.Features.Expenses.Query.GetExpense
{
    public class GetExpenseQueryResponse
    {
        public double Valor { get; set; }
        public string Descricao { get; set; }
    }
}

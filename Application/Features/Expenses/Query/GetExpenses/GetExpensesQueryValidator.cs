using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Application.Features.Expenses.Query.GetExpenses
{
    public class GetExpensesQueryValidator : AbstractValidator<GetExpensesQuery>
    {
        public GetExpensesQueryValidator()
        {
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Application.Features.Expenses.Command.EditExpenses
{
    public class EditExpensesCommandValidator : AbstractValidator<EditExpensesCommand>
    {
        public EditExpensesCommandValidator()
        {
        }
    }
}

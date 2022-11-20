using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Finance.Command.ExcludeGainsFinance
{
    public class ExcludeGainsFinanceCommandValidator : AbstractValidator<ExcludeGainsFinanceCommand>
    {
        public ExcludeGainsFinanceCommandValidator()
        {
        }
    }
}

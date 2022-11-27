using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Application.Features.Gains.Command.EditGains
{
    public class EditGainsCommandValidator : AbstractValidator<EditGainsCommand>
    {
        public EditGainsCommandValidator()
        {
        }
    }
}

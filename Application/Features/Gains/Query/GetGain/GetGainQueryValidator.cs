using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Application.Features.Gains.Query.GetGain
{
    public class GetGainQueryValidator : AbstractValidator<GetGainQuery>
    {
        public GetGainQueryValidator()
        {
        }
    }
}

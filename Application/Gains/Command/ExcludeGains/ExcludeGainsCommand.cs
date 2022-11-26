using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Gains.Command.ExcludeGains
{
    public class ExcludeGainsCommand : IRequest<ExcludeGainsCommandResponse>
    {
        public int Id { get; set; }
        public int FinancaId { get; set; }
    }
}

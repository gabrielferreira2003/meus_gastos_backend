using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Gains.Query.GetGains
{
    public class GetGainsQuery : IRequest<IEnumerable<GetGainsQueryResponse>>
    {
        public int FinancaId { get; set; }
    }
}

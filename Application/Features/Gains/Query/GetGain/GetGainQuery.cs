using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Application.Features.Gains.Query.GetGain
{
    public class GetGainQuery : IRequest<GetGainQueryResponse>
    {
        public int Id { get; set; }
    }
}

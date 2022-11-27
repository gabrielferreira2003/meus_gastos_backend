using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Application.Features.Finance.Query.SearchFinance
{
    public class GetFinanceQuery : IRequest<GetFinanceQueryResponse>
    {
        public Guid UsuarioId { get; set; }
    }
}

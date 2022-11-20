using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Finance.Query.SearchFinance
{
    public class GetFinanceQuery : IRequest<GetFinanceQueryResponse>
    {
        public Guid UsuarioId { get; set; }
    }
}

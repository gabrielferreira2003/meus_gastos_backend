using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Application.Features.Finance.Command.AddFinance
{
    public class AddFinanceCommand : IRequest<AddFinanceCommandResponse>
    {
        public double Patrimonio { get; set; }
        public Guid UsuarioId { get; set; }
    }
}

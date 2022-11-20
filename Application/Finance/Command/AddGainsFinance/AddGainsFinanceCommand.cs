using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Finance.Command.AddGainsFinance
{
    public class AddGainsFinanceCommand : IRequest<AddGainsFinanceCommandResponse>
    {
        public int Id { get; set; }
        public double Ganhos { get; set; }
        public double Patrimonio { get; set; }
        public Guid UsuarioId { get; set; }
    }
}

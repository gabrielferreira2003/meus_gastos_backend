using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Finance.Command.ExcludeGainsFinance
{
    public class ExcludeGainsFinanceCommand : IRequest<ExcludeGainsFinanceCommandResponse>
    {
        public int Id { get; set; }
        public double Ganhos { get; set; }
        public double Patrimonio { get; set; }
        public Guid UsuarioId { get; set; }
    }
}

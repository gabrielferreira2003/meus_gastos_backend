using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Application.Features.Finance.Command.AddValueFinance
{
    public class AddValueFinanceCommand : IRequest<AddValueFinanceCommandResponse>
    {
        public int Id { get; set; }
        public double Valor { get; set; }
    }
}

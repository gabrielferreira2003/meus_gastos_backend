using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Application.Features.Gains.Command.AddGains
{
    public class AddGainsCommand : IRequest<AddGainsCommandResponse>
    {
        public double Valor { get; set; }
        public string Descricao { get; set; }
    }
}

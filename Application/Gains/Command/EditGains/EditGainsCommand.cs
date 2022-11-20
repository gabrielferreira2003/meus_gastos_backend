using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Gains.Command.EditGains
{
    public class EditGainsCommand : IRequest<EditGainsCommandResponse>
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
        public DateTime Hora { get; set; }
        public int FinancasId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Gains.Query.GetGains
{
    public class GetGainsQueryResponse
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
        public DateTime Hora { get; set; }
    }
}

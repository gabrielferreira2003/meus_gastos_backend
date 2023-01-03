using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Application.Features.Gains.Command.AddGainsExcel
{
    public class AddGainsExcelCommand : IRequest<AddGainsExcelCommandResponse>
    {
        public IFormFile Arquivo { get; set; }
    }
}

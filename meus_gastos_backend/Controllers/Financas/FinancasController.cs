using MeusGastos.Application.Features.Finance.Command.AddFinance;
using MeusGastos.Application.Features.Finance.Query.SearchFinance;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeusGastos.Controllers.Financas
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class FinancasController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FinancasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<AddFinanceCommandResponse> AdicionarFinancas([FromBody] AddFinanceCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<GetFinanceQueryResponse> BuscarFinancas([FromQuery] GetFinanceQuery request)
        {
            return await _mediator.Send(request);
        }
    }
}

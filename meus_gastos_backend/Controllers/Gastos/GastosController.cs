using MeusGastos.Application.Features.Expenses.Command.AddExpenses;
using MeusGastos.Application.Features.Expenses.Command.DeleteExpenses;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MeusGastos.Application.Features.Expenses.Query.GetExpenses;
using MeusGastos.Application.Features.Expenses.Command.EditExpenses;

namespace MeusGastos.Controllers.Gastos
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class GastosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GastosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<AddExpensesCommandResponse> AdiconarGastos([FromBody] AddExpensesCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<DeleteExpensesCommandResponse> DeletarGastos([FromBody] DeleteExpensesCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IEnumerable<GetExpensesQueryResponse>> PegarGastos([FromQuery] GetExpensesQuery request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<EditExpensesCommandResponse> EditarGastos([FromBody] EditExpensesCommand request)
        {
            return await _mediator.Send(request);
        }
    }
}

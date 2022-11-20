using Application.Expenses.Command.AddExpenses;
using Application.Expenses.Command.DeleteExpenses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace meus_gastos_backend.Controllers.Gastos
{
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
    }
}

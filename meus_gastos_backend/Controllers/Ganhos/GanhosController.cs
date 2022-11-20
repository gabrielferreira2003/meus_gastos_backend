using Application.Gains.Command.AddGains;
using Application.Gains.Command.EditGains;
using Application.Gains.Command.ExcludeGains;
using Application.Gains.Query.GetGains;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace meus_gastos_backend.Controllers.Ganhos
{
    [Route("api/[controller]")]
    [ApiController]
    public class GanhosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GanhosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<AddGainsCommandResponse> AdicionarGanhos([FromBody] AddGainsCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<ExcludeGainsCommandResponse> DeletarGanhos([FromBody] ExcludeGainsCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IEnumerable<GetGainsQueryResponse>> PegarGanhos([FromQuery] GetGainsQuery request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<EditGainsCommandResponse> EditarGanhos([FromQuery] EditGainsCommand request)
        {
            return await _mediator.Send(request);
        }
    }
}

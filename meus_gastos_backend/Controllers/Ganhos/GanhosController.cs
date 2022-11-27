using MeusGastos.Application.Features.Gains.Command.AddGains;
using MeusGastos.Application.Features.Gains.Command.EditGains;
using MeusGastos.Application.Features.Gains.Command.ExcludeGains;
using MeusGastos.Application.Features.Gains.Query.GetGains;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeusGastos.Controllers.Ganhos
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public async Task<EditGainsCommandResponse> EditarGanhos([FromBody] EditGainsCommand request)
        {
            return await _mediator.Send(request);
        }
    }
}

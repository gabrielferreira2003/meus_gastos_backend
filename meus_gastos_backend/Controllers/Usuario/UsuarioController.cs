using Application.User.Command.RegisterUser;
using Application.User.Command.UserLogin;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace meus_gastos_backend.Controllers.Usuario
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<RegisterUserCommandResponse> CadastroUsuario([FromBody] RegisterUserCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("[action]")]
        public async Task<UserLoginCommandResponse> LoginUsuario([FromBody] UserLoginCommand request)
        {
            return await _mediator.Send(request);
        }
    }
}

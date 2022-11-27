using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Application.Features.User.Command.UserLogin
{
    public class UserLoginCommand : IRequest<UserLoginCommandResponse>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}

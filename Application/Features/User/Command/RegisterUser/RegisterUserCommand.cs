using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Application.Features.User.Command.RegisterUser
{
    public class RegisterUserCommand : IRequest<RegisterUserCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassowrd { get; set; }
        public double CapitalInicial { get; set; }
    }
}

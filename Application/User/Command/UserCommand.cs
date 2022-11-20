using Application.Finance.Command.AddFinance;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Command
{
    public class UserCommand
    {
        public UserCommand()
        {
            this.Financas = new AddFinanceCommand();
        }

        public string UserName { get; set; }

        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        public AddFinanceCommand Financas { get; set; }

        public static implicit operator ApplicationUser(UserCommand insertUser)
            => new()
            {
                UserName = insertUser.UserName,
                Email = insertUser.Email,
                EmailConfirmed = insertUser.EmailConfirmed,
            };
    }
}

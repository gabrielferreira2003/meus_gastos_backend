using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Domain.Entidades
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Financas = new HashSet<Financas>();
            
        }

        public virtual ICollection<Financas> Financas { get; set; }
    }
}

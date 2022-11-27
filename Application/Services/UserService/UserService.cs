using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Application.Services.UserService
{
    public interface IUserService
    {
        ClaimsPrincipal GetUser();
        Guid GetUserId();
        
    }

    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _accessor;
        public UserService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public ClaimsPrincipal GetUser()
        {
            return (_accessor?.HttpContext?.User as ClaimsPrincipal)!;
        }
        public Guid GetUserId()
        {
            var user = GetUser();
            if (user?.Identity?.Name is null) return Guid.Empty;
            return Guid.Parse(user.Identity.Name);
        }
    }
}

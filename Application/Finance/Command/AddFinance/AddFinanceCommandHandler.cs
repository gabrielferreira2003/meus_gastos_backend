using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastruture.Contexto;
using Domain.Entidades;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Application.Finance.Command.AddFinance
{
    public class AddFinanceCommandHandler : IRequestHandler<AddFinanceCommand, AddFinanceCommandResponse>
    {
        private readonly Contexto _contexto;
        private readonly IHttpContextAccessor _accessor;
        public AddFinanceCommandHandler(Contexto contexto, IHttpContextAccessor accessor)
        {
            _contexto = contexto;
            _accessor = accessor;
        }

        public async Task<AddFinanceCommandResponse> Handle(AddFinanceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var teste = GetUserId();

                var financasAdicionar = new Financas()
                {
                    Patrimonio = request.Patrimonio,
                    UsuarioId = request.UsuarioId,
                };

                _contexto.Financas.Add(financasAdicionar);
                await _contexto.SaveChangesAsync();

                return new AddFinanceCommandResponse() { Sucesso = true };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Guid GetUserId()
        {
            var user = _accessor?.HttpContext?.User as ClaimsPrincipal;
            if (user?.Identity?.Name is null) return Guid.Empty;
            return Guid.Parse(user.Identity.Name);
        }

    }
}

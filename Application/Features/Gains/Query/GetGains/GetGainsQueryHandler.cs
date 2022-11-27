using MediatR;
using MeusGastos.Application.Services.UserService;
using MeusGastos.Infrastructure.Contexto;
using Microsoft.EntityFrameworkCore;

namespace MeusGastos.Application.Features.Gains.Query.GetGains
{
    public class GetGainsQueryHandler : IRequestHandler<GetGainsQuery, IEnumerable<GetGainsQueryResponse>>
    {
        private readonly Contexto _contexto;
        private readonly IUserService _userService;
        public GetGainsQueryHandler(Contexto contexto,
            IUserService userService)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<IEnumerable<GetGainsQueryResponse>?> Handle(GetGainsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var usuarioSelecionado = await _contexto.ApplicationUser.FirstAsync(u => u.Id == _userService.GetUserId(), cancellationToken);

                return await _contexto.Ganhos
                    .Where(g => g.FinancasId == usuarioSelecionado.Financas.First().Id)
                    .Select(g => new GetGainsQueryResponse()
                    {
                        Id = g.Id,
                        Valor = g.Valor,
                        Descricao = g.Descricao,
                        Hora = g.Hora,
                    })
                    .OrderByDescending(d => d.Hora)
                    .ToListAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

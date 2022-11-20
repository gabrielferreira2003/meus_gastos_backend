using MediatR;
using Infrastruture.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Application.Gains.Query.GetGains
{
    public class GetGainsQueryHandler : IRequestHandler<GetGainsQuery, IEnumerable<GetGainsQueryResponse>>
    {
        private readonly Contexto _contexto;
        public GetGainsQueryHandler(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<GetGainsQueryResponse>?> Handle(GetGainsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _contexto.Ganhos
                    .Where(g => g.FinancasId == request.FinancaId)
                    .Select(g => new GetGainsQueryResponse()
                    {
                        Id = g.Id,
                        Valor = g.Valor,
                        Descricao = g.Descricao,
                        Hora = g.Hora,
                    })
                    .OrderByDescending(d => d.Hora)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

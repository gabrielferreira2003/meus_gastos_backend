using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeusGastos.Infrastructure.Contexto;
using Microsoft.EntityFrameworkCore;

namespace MeusGastos.Application.Features.Gains.Query.GetGain
{
    public class GetGainQueryHandler : IRequestHandler<GetGainQuery, GetGainQueryResponse>
    {
        private readonly Contexto _contexto;
        public GetGainQueryHandler(Contexto contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public async Task<GetGainQueryResponse> Handle(GetGainQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var ganhoSelecionado = await _contexto.Ganhos.FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

                return new GetGainQueryResponse()
                {
                    Valor = ganhoSelecionado.Valor,
                    Descricao = ganhoSelecionado.Descricao
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

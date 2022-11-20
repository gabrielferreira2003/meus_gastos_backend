using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastruture.Contexto;
using Microsoft.AspNetCore.Identity;
using Domain.Entidades;
using Infrastruture.Extencoes;

namespace Application.Finance.Query.SearchFinance
{
    public class GetFinanceQueryHandler : IRequestHandler<GetFinanceQuery, GetFinanceQueryResponse>
    {
        private readonly Contexto _contexto;
        public GetFinanceQueryHandler(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<GetFinanceQueryResponse> Handle(GetFinanceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var financaSelecionada = _contexto.Financas.FirstOrDefault(f => f.UsuarioId == request.UsuarioId);
                if (financaSelecionada == null) throw new Exception("Finança não encontrada!");

                double somaGanhos = SomarArray.Ganhos(financaSelecionada.Ganhos);
                double somaGastos = SomarArray.Gastos(financaSelecionada.Gastos);

                var financaRetorno = new GetFinanceQueryResponse()
                {
                    Id = financaSelecionada.Id,
                    Patrimonio = financaSelecionada.Patrimonio,
                    Ganhos = somaGanhos,
                    Gastos = somaGastos,
                };

                return financaRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

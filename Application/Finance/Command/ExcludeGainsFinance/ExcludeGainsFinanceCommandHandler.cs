using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastruture.Contexto;
using Domain.Entidades;

namespace Application.Finance.Command.ExcludeGainsFinance
{
    public class ExcludeGainsFinanceCommandHandler : IRequestHandler<ExcludeGainsFinanceCommand, ExcludeGainsFinanceCommandResponse>
    {
        private readonly Contexto _contexto;
        public ExcludeGainsFinanceCommandHandler(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<ExcludeGainsFinanceCommandResponse> Handle(ExcludeGainsFinanceCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var financaAtualizada = new Financas()
                {
                    Id = request.Id,
                    Patrimonio = request.Patrimonio - request.Ganhos,
                    UsuarioId = request.UsuarioId,
                };

                _contexto.Financas.Update(financaAtualizada);
                await _contexto.SaveChangesAsync();

                return new ExcludeGainsFinanceCommandResponse() { Sucesso = true };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

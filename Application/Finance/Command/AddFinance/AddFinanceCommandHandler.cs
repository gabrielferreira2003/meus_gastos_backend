using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastruture.Contexto;
using Domain.Entidades;

namespace Application.Finance.Command.AddFinance
{
    public class AddFinanceCommandHandler : IRequestHandler<AddFinanceCommand, AddFinanceCommandResponse>
    {
        private readonly Contexto _contexto;
        public AddFinanceCommandHandler(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<AddFinanceCommandResponse> Handle(AddFinanceCommand request, CancellationToken cancellationToken)
        {
            try
            {
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
    }
}

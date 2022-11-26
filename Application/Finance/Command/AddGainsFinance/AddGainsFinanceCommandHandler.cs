using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastruture.Contexto;
using Microsoft.EntityFrameworkCore;
using Domain.Entidades;

namespace Application.Finance.Command.AddGainsFinance
{
    public class AddGainsFinanceCommandHandler : IRequestHandler<AddGainsFinanceCommand, AddGainsFinanceCommandResponse>
    {
        private readonly Contexto _contexto;
        public AddGainsFinanceCommandHandler(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<AddGainsFinanceCommandResponse> Handle(AddGainsFinanceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var financaSelecionada = await _contexto.Financas.FirstOrDefaultAsync(x => x.Id == request.Id);

                financaSelecionada!.Patrimonio = financaSelecionada.Patrimonio + request.Ganhos;

                _contexto.Financas.Update(financaSelecionada);
                await _contexto.SaveChangesAsync(cancellationToken);

                return new AddGainsFinanceCommandResponse() { Sucesso = true };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

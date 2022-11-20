using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastruture.Contexto;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Application.Gains.Command.EditGains
{
    public class EditGainsCommandHandler : IRequestHandler<EditGainsCommand, EditGainsCommandResponse>
    {
        private readonly Contexto _contexto;
        public EditGainsCommandHandler(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<EditGainsCommandResponse> Handle(EditGainsCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var ganhosEnviar = new Ganhos()
                {
                    Id = request.Id,
                    Valor = request.Valor,
                    Descricao = request.Descricao,
                    Hora = request.Hora,
                    FinancasId = request.FinancasId,
                };

                _contexto.Update(ganhosEnviar);
                await _contexto.SaveChangesAsync();

                return new EditGainsCommandResponse() { Sucesso = true };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using System.Threading;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastruture.Contexto;
using Application.Finance.Command.AddGainsFinance;
using Domain.Entidades;

namespace Application.Gains.Command.AddGains
{
    public class AddGainsCommandHandler : IRequestHandler<AddGainsCommand, AddGainsCommandResponse>
    {
        private readonly Contexto _contexto;
        private readonly IMediator _mediator;
        public AddGainsCommandHandler(Contexto contexto, IMediator mediator)
        {
            _contexto = contexto;
            _mediator = mediator;
        }

        public async Task<AddGainsCommandResponse> Handle(AddGainsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ganhosAdicionar = new Ganhos()
                { 
                    Valor = request.Valor,
                    Descricao = request.Descricao,
                    Hora = DateTime.UtcNow,
                    FinancasId = request.FinancasId,
                };

                await _contexto.Ganhos.AddAsync(ganhosAdicionar);
                await _contexto.SaveChangesAsync();

                var parametroSomarGanhos = new AddGainsFinanceCommand()
                {
                    Id = request.FinancasId,
                    Ganhos = request.Valor,
                    Patrimonio = request.Patrimonio,
                    UsuarioId = request.UsuarioId,
                };

                var resposta = await _mediator.Send(parametroSomarGanhos);

                return new AddGainsCommandResponse() { Sucesso = true };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeusGastos.Infrastructure.Contexto;
using MeusGastos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using MeusGastos.Application.Services.UserService;
using MeusGastos.Application.Features.Finance.Command.AddValueFinance;

namespace MeusGastos.Application.Features.Gains.Command.EditGains
{
    public class EditGainsCommandHandler : IRequestHandler<EditGainsCommand, EditGainsCommandResponse>
    {
        private readonly Contexto _contexto;
        private readonly IMediator _mediator;
        private readonly IUserService _userService;
        public EditGainsCommandHandler(Contexto contexto,
            IMediator mediator,
            IUserService userService)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<EditGainsCommandResponse> Handle(EditGainsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var usuarioSelecionado = await _contexto.ApplicationUser.FirstOrDefaultAsync(f => f.Id == _userService.GetUserId(), cancellationToken);
                if (usuarioSelecionado == null) throw new Exception("Usuário não encontrado!");

                var ganhoSelecionado = await _contexto.Ganhos.FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
                if (ganhoSelecionado == null) throw new Exception("Ganho não encontrado!");

                var parametroSomarGanhos = new AddValueFinanceCommand()
                { 
                    Id = usuarioSelecionado.Financas.First().Id,
                    Valor = request.Valor - ganhoSelecionado.Valor
            };

                await _mediator.Send(parametroSomarGanhos, cancellationToken);

                ganhoSelecionado.Valor = request.Valor;
                ganhoSelecionado.Descricao = request.Descricao;

                _contexto.Update(ganhoSelecionado);
                await _contexto.SaveChangesAsync(cancellationToken);

                return new EditGainsCommandResponse() { Sucesso = true };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

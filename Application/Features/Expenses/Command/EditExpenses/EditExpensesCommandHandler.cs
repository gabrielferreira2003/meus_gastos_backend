using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeusGastos.Infrastructure.Contexto;
using Microsoft.EntityFrameworkCore;
using MeusGastos.Application.Features.Finance.Command.AddValueFinance;
using MeusGastos.Application.Services.UserService;

namespace MeusGastos.Application.Features.Expenses.Command.EditExpenses
{
    public class EditExpensesCommandHandler : IRequestHandler<EditExpensesCommand, EditExpensesCommandResponse>
    {
        private readonly Contexto _contexto;
        private readonly IMediator _mediator;
        private readonly IUserService _userService;
        public EditExpensesCommandHandler(Contexto contexto,
            IMediator mediator,
            IUserService userService)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<EditExpensesCommandResponse> Handle(EditExpensesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var usuarioSelecionado = await _contexto.ApplicationUser.FirstOrDefaultAsync(u => u.Id == _userService.GetUserId(), cancellationToken);

                var gastoSelecionado = await _contexto.Gastos.FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

                var gastoFinanca = new AddValueFinanceCommand()
                { 
                    Id = usuarioSelecionado.Financas.First().Id,
                    Valor = gastoSelecionado.Valor - request.Valor
                };

                await _mediator.Send(gastoFinanca, cancellationToken);

                gastoSelecionado.Valor = request.Valor;
                gastoSelecionado.Descricao = request.Descricao;

                _contexto.Update(gastoSelecionado);
                await _contexto.SaveChangesAsync(cancellationToken);

                return new EditExpensesCommandResponse() { Sucesso = true };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

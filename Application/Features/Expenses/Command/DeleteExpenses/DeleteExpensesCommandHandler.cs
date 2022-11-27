using MediatR;
using MeusGastos.Application.Features.Finance.Command.AddValueFinance;
using MeusGastos.Application.Services.UserService;
using MeusGastos.Infrastructure.Contexto;
using Microsoft.EntityFrameworkCore;

namespace MeusGastos.Application.Features.Expenses.Command.DeleteExpenses
{
    public class DeleteExpensesCommandHandler : IRequestHandler<DeleteExpensesCommand, DeleteExpensesCommandResponse>
    {
        private readonly Contexto _contexto;
        private readonly IUserService _userService;
        private readonly IMediator _mediator;
        public DeleteExpensesCommandHandler(Contexto contexto,
            IUserService userService,
            IMediator mediator)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<DeleteExpensesCommandResponse> Handle(DeleteExpensesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var usuarioSelecionado = await _contexto.ApplicationUser.FirstOrDefaultAsync(u => u.Id == _userService.GetUserId(), cancellationToken);

                var gastoSelecionado = await _contexto.Gastos.FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
                if (gastoSelecionado == null) { throw new Exception("Gasto não encontrado!"); }

                var gastoFinanca = new AddValueFinanceCommand()
                {
                    Id = usuarioSelecionado!.Financas.First().Id,
                    Valor = gastoSelecionado.Valor
                };

                await _mediator.Send(gastoFinanca, cancellationToken);

                _contexto.Gastos.Remove(gastoSelecionado);
                await _contexto.SaveChangesAsync(cancellationToken);

                return new DeleteExpensesCommandResponse() { Sucesso = true };
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}

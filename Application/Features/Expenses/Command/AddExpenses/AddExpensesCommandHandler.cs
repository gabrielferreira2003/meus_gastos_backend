using MediatR;
using MeusGastos.Infrastructure.Contexto;
using MeusGastos.Domain.Entidades;
using MeusGastos.Application.Features.Finance.Command.SubtractExpensesFinance;
using Microsoft.EntityFrameworkCore;
using MeusGastos.Application.Services.UserService;
using MeusGastos.Application.Features.Finance.Command.AddValueFinance;

namespace MeusGastos.Application.Features.Expenses.Command.AddExpenses
{
    public class AddExpensesCommandHandler : IRequestHandler<AddExpensesCommand, AddExpensesCommandResponse>
    {
        private readonly Contexto _contexto;
        private readonly IMediator _mediator;
        private readonly IUserService _userService;

        public AddExpensesCommandHandler(Contexto contexto, 
            IMediator mediator,
            IUserService userService)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<AddExpensesCommandResponse> Handle(AddExpensesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var usuarioSelecionado = await _contexto.ApplicationUser.FirstOrDefaultAsync(f => f.Id == _userService.GetUserId(), cancellationToken);

                var gastosAdicionar = new Gastos()
                { 
                    Valor = request.Valor,
                    Descricao = request.Descricao,
                    Hora = DateTime.UtcNow,
                    FinancasId = usuarioSelecionado!.Financas.First().Id,
                };

                _contexto.Gastos.Add(gastosAdicionar);
                await _contexto.SaveChangesAsync(cancellationToken);

                var parametroSubtrairGastos = new AddValueFinanceCommand()
                {
                    Id = usuarioSelecionado!.Financas.First().Id,
                    Valor = - request.Valor,
                };

                await _mediator.Send(parametroSubtrairGastos, cancellationToken);

                return new AddExpensesCommandResponse() { Sucesso = true };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

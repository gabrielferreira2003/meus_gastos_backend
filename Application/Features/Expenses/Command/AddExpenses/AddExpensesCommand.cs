using MediatR;

namespace MeusGastos.Application.Features.Expenses.Command.AddExpenses;

public class AddExpensesCommand : IRequest<AddExpensesCommandResponse>
{
    public double Valor { get; set; }
    public string Descricao { get; set; }
}

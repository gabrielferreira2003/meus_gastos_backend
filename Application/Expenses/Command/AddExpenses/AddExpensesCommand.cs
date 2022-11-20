using MediatR;

namespace Application.Expenses.Command.AddExpenses;

public class AddExpensesCommand : IRequest<AddExpensesCommandResponse>
{
    public double Valor { get; set; }
    public string Descricao { get; set; }
    public int FinancasId { get; set; }
}

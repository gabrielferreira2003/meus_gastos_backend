using MeusGastos.Domain.Entidades;

namespace MeusGastos.Application.Features.Finance.Query.SearchFinance
{
    public class GetFinanceQueryResponse
    {
        public int Id { get; set; }
        public double Patrimonio { get; set; }
        public double Ganhos { get; set; }
        public double Gastos { get; set; }
    }
}

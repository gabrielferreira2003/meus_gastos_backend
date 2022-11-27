using MeusGastos.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Infrastructure.Extencoes
{
    public static class SomarArray
    {
        public static double Ganhos(List<Ganhos> ganhos)
        {
            double somaGanhos = 0;
            foreach (Ganhos ganho in ganhos)
            {
                somaGanhos += ganho.Valor;
            }

            return somaGanhos;
        }

        public static double Gastos(List<Gastos> gastos)
        {
            double somaGastos = 0;
            foreach (Gastos gasto in gastos)
            {
                somaGastos += gasto.Valor;
            }

            return somaGastos;
        }
    }
}

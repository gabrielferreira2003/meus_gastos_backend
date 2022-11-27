﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Application.Features.Expenses.Query.GetExpense
{
    public class GetExpenseQuery : IRequest<GetExpenseQueryResponse>
    {
        public int Id { get; set; }
    }
}

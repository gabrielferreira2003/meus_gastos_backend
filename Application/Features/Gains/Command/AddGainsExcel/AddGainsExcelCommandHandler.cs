using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeusGastos.Infrastructure.Contexto;
using OfficeOpenXml;
using MeusGastos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using MeusGastos.Application.Services.UserService;

namespace MeusGastos.Application.Features.Gains.Command.AddGainsExcel
{
    public class AddGainsExcelCommandHandler : IRequestHandler<AddGainsExcelCommand, AddGainsExcelCommandResponse>
    {
        private readonly Contexto _contexto;
        private readonly IUserService _userService;
        public AddGainsExcelCommandHandler(Contexto contexto,
            IUserService userService)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<AddGainsExcelCommandResponse> Handle(AddGainsExcelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var usuarioSelecionado = await _contexto.ApplicationUser.FirstOrDefaultAsync(u => u.Id == _userService.GetUserId(), cancellationToken);

                FileInfo existing = new(request.Arquivo.FileName);

                using ExcelPackage package = new(existing);

                ExcelWorksheet worsheet = package.Workbook.Worksheets[0];

                int colCount = worsheet.Dimension.End.Column;
                int rowCount = worsheet.Dimension.End.Row;

                for (int row = 2; row <= rowCount; row++)
                {
                    var ganho = new Ganhos()
                    {
                        Valor = Convert.ToDouble(worsheet.Cells[row, 1].Value.ToString()),
                        Descricao = worsheet.Cells[row, 2].Value.ToString()!,
                        Hora = DateTime.UtcNow,
                        FinancasId = usuarioSelecionado!.Financas.First().Id,
                    };

                    _contexto.Ganhos.Add(ganho);
                    await _contexto.SaveChangesAsync(cancellationToken);
                }

                return new AddGainsExcelCommandResponse();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MeusGastos.Domain.Entidades
{
    [Table("Ganhos")]
    public class Ganhos
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo valor é obrigatório")]
        public double Valor { get; set; }

        [StringLength(500, ErrorMessage = "O gênero não pode passar de 500 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo hora é obrigatório")]
        public DateTime Hora { get; set; }

        [JsonIgnore]
        public virtual Financas Financas { get; set; }

        [JsonIgnore]
        public int FinancasId { get; set; }
    }
}

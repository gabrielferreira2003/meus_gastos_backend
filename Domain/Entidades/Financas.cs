using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    [Table("Financas")]
    public class Financas
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public double Patrimonio { get; set; }
        [JsonIgnore]
        public virtual List<Ganhos> Ganhos { get; set; }
        [JsonIgnore]
        public virtual List<Gastos> Gastos { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser Usuario { get; set; }
        public Guid UsuarioId { get; set; }
    }
}

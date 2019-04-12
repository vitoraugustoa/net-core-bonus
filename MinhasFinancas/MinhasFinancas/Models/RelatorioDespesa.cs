using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasFinancas.Models
{
    public class RelatorioDespesa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome do Item")]
        [DataType(DataType.Text)]
        [Column(TypeName = "nvarchar(100)")]
        public string ItemNome { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Valor { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Data da Despesa")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}" , ApplyFormatInEditMode = true)]
        public DateTime DataDespesa { get; set; }

        [Required]
        [StringLength(100)]
        public string Categoria { get; set; }
    }
}

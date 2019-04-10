using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspCore_SalvaExibeImagem.Models
{
    [Table("filmes")]
    public class Filme
    {
        [Column("id")]
        public int FilmeId { get; set; }
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Data de Lançamento")]
        public DateTime DataLancamento { get; set; }

        [Display(Name = "Genero")]
        public string Genero { get; set; }

        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        public byte[] Imagem { get; set; }
    }
}

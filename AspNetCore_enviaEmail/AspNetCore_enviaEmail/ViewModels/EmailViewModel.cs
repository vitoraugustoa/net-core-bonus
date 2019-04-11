using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore_enviaEmail.ViewModels
{
    public class EmailViewModel
    {
        [Required]
        [Display(Name ="Email de destino"), EmailAddress]
        public string Destino { get; set; }

        [Required]
        [Display(Name = "Assunto")]
        public string Assunto { get; set; }

        [Required]
        [Display(Name = "Mensagem")]
        public string Mensagem { get; set; }
    }
}

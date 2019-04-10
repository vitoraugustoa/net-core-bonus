using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCore_PDF.Models;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace AspCore_PDF.Controllers
{
    public class PDFController : Controller
    {
        public IActionResult PDF_Exemplo1()
        {
            return new ViewAsPdf("PDF_Exemplo1"); // Exemplo simples sem configura o arquivo PDF
        }

        public IActionResult PDF_Exemplo2()
        {
            ViewAsPdf pdf = new ViewAsPdf("PDF_Exemplo1")
            {
                FileName = "Nome_do_Arquivo",
                PageMargins = { Left = 20 , Bottom = 20 , Right = 20 , Top = 20 } , // Margem das paginas
                PageSize = Rotativa.AspNetCore.Options.Size.A4, // Tamanho das paginas
                CustomSwitches = "--footer-center \"  Criado Em: " +
                 DateTime.Now.Date.ToString("dd/MM/yyyy") + "  Pág.: [page]/[toPage]\"" +
                 " --footer-line --footer-font-size \"12\" --footer-spacing 1 --footer-font-name \"Segoe UI\""
            };

            return pdf;
        }

        public IActionResult RelacaoClientesPDF()
        {

            List<Cliente> listaClientes = new List<Cliente>()
            {
               new Cliente { Nome = "Macoratti", Endereco = "Rua America , 100", Cidade = "Lins",  CPF = "111.222.333-99", Email = "macoratti@yahoo.com" },
               new Cliente { Nome = "Jefferson", Endereco = "Av. México 230", Cidade = "Campinas",  CPF = "222.222.333-99", Email = "jefferson@yahoo.com" },
               new Cliente { Nome = "Jessica", Endereco = "Rua Projetada 2309", Cidade = "Sorocaba",  CPF = "333.222.333-99", Email = "jessica@yahoo.com" },
               new Cliente { Nome = "Janice", Endereco = "Rua XV de Novembro , 34", Cidade = "Piracicaba",  CPF = "444.222.333-99", Email = "janice@yahoo.com" },
               new Cliente { Nome = "Miriam", Endereco = "Pça da Luz, 30", Cidade = "Catanduva",  CPF = "555.222.333-99", Email = "miriam@yahoo.com" },
            };

            return new ViewAsPdf("RelacaoClientesPDF" , listaClientes);
        }
    }
}
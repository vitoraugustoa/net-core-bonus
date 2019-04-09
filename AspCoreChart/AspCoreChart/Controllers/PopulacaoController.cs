using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreChart.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreChart.Controllers
{
    public class PopulacaoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult PopulacaoGrafico()
        {
            var listaPopulacao = PopulacaoService.GetPopulacaPorEstado();
            return Json(listaPopulacao);
        }
    }
}
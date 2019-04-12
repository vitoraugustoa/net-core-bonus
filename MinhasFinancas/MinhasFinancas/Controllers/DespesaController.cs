using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MinhasFinancas.DAL;
using MinhasFinancas.Models;

namespace MinhasFinancas.Controllers
{
    public class DespesaController : Controller
    {
        private readonly IFinancasDAL _financasDal;
        public DespesaController(IFinancasDAL financasDal)
        {
            _financasDal = financasDal;
        }

        [HttpGet]
        public IActionResult Index(string criterio)
        {
            List<RelatorioDespesa> listDespesas = _financasDal.GetAllDespesas().ToList();

            if (!String.IsNullOrEmpty(criterio))
                listDespesas = _financasDal.GetFiltraDespesas(criterio).ToList();

            return View(listDespesas);
        }

        [HttpGet]
        public IActionResult AddEditDespesa(int itemId)
        {
            RelatorioDespesa model = new RelatorioDespesa();
            if (itemId > 0)
            {
                model = _financasDal.GetDespesa(itemId);
            }
            return PartialView("_despesaForm" , model);
        }

        [HttpPost]
        public IActionResult Create(RelatorioDespesa novaDepesa)
        {
            if (ModelState.IsValid)
            {
                if (novaDepesa.Id > 0)
                {
                    _financasDal.UpdateDespesa(novaDepesa);
                }
                else
                {
                    _financasDal.AddDespesas(novaDepesa);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _financasDal.DeleteDespesa(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult DespesaResumo()
        {
            return PartialView("_despesaReport");
        }

        [HttpGet]
        public JsonResult GetDespesaPorPeriodo()
        {
            Dictionary<string , decimal> despesaPeriodo = _financasDal.CalculaDespesaPeriodo(1);
            return new JsonResult(despesaPeriodo);
        }

        [HttpGet]
        public JsonResult GetDespesaPorPeriodoSemanal()
        {
            Dictionary<string , decimal> despesaPeriodoSemanal = _financasDal.CalculaDespesaPeriodoSemanal(7);
            return new JsonResult(despesaPeriodoSemanal);
        }
    }
}
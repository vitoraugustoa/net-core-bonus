using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MinhasFinancas.Context;
using MinhasFinancas.Models;

namespace MinhasFinancas.DAL
{
    public class FinancasDAL : IFinancasDAL
    {
        public FinancasDAL() { }

        private readonly AppDbContext _context;
        public FinancasDAL(AppDbContext context)
        {
            _context = context;
        }

        public void AddDespesas(RelatorioDespesa despesa)
        {
            try
            {
                _context.RelatorioDespesas.Add(despesa);
                _context.SaveChanges();
            }
            catch { throw; }
        }

        public Dictionary<string , decimal> CalculaDespesaPeriodo(int periodo)
        {
            Dictionary<string , decimal> SomaDespesasPeriodo = new Dictionary<string , decimal>();

            decimal despAlimentacao = _context.RelatorioDespesas.Where
                                (cat => cat.Categoria == "Alimentacao" && (cat.DataDespesa > DateTime.Now.AddMonths(-periodo)))
                                .Select(cat => cat.Valor)
                                .Sum();

            decimal despCompras = _context.RelatorioDespesas.Where
                               (cat => cat.Categoria == "Compras" && (cat.DataDespesa > DateTime.Now.AddMonths(-periodo)))
                               .Select(cat => cat.Valor)
                               .Sum();

            decimal despTransporte = _context.RelatorioDespesas.Where
                               (cat => cat.Categoria == "Transporte" && (cat.DataDespesa > DateTime.Now.AddMonths(-periodo)))
                               .Select(cat => cat.Valor)
                               .Sum();

            decimal despSaude = _context.RelatorioDespesas.Where
                               (cat => cat.Categoria == "Saude" && (cat.DataDespesa > DateTime.Now.AddMonths(-periodo)))
                               .Select(cat => cat.Valor)
                               .Sum();

            decimal despMoradia = _context.RelatorioDespesas.Where
                               (cat => cat.Categoria == "Moradia" && (cat.DataDespesa > DateTime.Now.AddMonths(-periodo)))
                               .Select(cat => cat.Valor)
                               .Sum();

            decimal despLazer = _context.RelatorioDespesas.Where
                               (cat => cat.Categoria == "Lazer" && (cat.DataDespesa > DateTime.Now.AddMonths(-periodo)))
                               .Select(cat => cat.Valor)
                               .Sum();

            SomaDespesasPeriodo.Add("Alimentacao" , despAlimentacao);
            SomaDespesasPeriodo.Add("Compras" , despCompras);
            SomaDespesasPeriodo.Add("Transporte" , despTransporte);
            SomaDespesasPeriodo.Add("Saude" , despSaude);
            SomaDespesasPeriodo.Add("Moradia" , despMoradia);
            SomaDespesasPeriodo.Add("Lazer" , despLazer);

            return SomaDespesasPeriodo;
        }

        public Dictionary<string , decimal> CalculaDespesaPeriodoSemanal(int periodo)
        {
            Dictionary<string , decimal> SomaDespesasPeriodoSemanal = new Dictionary<string , decimal>();

            decimal despAlimentacao = _context.RelatorioDespesas.Where
                                (cat => cat.Categoria == "Alimentacao" && (cat.DataDespesa > DateTime.Now.AddDays(-periodo)))
                                .Select(cat => cat.Valor)
                                .Sum();

            decimal despCompras = _context.RelatorioDespesas.Where
                               (cat => cat.Categoria == "Compras" && (cat.DataDespesa > DateTime.Now.AddDays(-periodo)))
                               .Select(cat => cat.Valor)
                               .Sum();

            decimal despTransporte = _context.RelatorioDespesas.Where
                               (cat => cat.Categoria == "Transporte" && (cat.DataDespesa > DateTime.Now.AddDays(-periodo)))
                               .Select(cat => cat.Valor)
                               .Sum();

            decimal despSaude = _context.RelatorioDespesas.Where
                               (cat => cat.Categoria == "Saude" && (cat.DataDespesa > DateTime.Now.AddDays(-periodo)))
                               .Select(cat => cat.Valor)
                               .Sum();

            decimal despMoradia = _context.RelatorioDespesas.Where
                               (cat => cat.Categoria == "Moradia" && (cat.DataDespesa > DateTime.Now.AddDays(-periodo)))
                               .Select(cat => cat.Valor)
                               .Sum();
            decimal despLazer = _context.RelatorioDespesas.Where
                               (cat => cat.Categoria == "Lazer" && (cat.DataDespesa > DateTime.Now.AddDays(-periodo)))
                               .Select(cat => cat.Valor)
                               .Sum();

            SomaDespesasPeriodoSemanal.Add("Alimentacao" , despAlimentacao);
            SomaDespesasPeriodoSemanal.Add("Compras" , despCompras);
            SomaDespesasPeriodoSemanal.Add("Transporte" , despTransporte);
            SomaDespesasPeriodoSemanal.Add("Saude" , despSaude);
            SomaDespesasPeriodoSemanal.Add("Moradia" , despMoradia);
            SomaDespesasPeriodoSemanal.Add("Lazer" , despLazer);

            return SomaDespesasPeriodoSemanal;
        }

        public void DeleteDespesa(int id)
        {
            try
            {
                RelatorioDespesa desp = _context.RelatorioDespesas.Find(id);
                _context.RelatorioDespesas.Remove(desp);
                _context.SaveChanges();
            }
            catch { throw; }
        }

        public IEnumerable<RelatorioDespesa> GetAllDespesas()
        {
            try
            {
                return _context.RelatorioDespesas.ToList();
            }
            catch
            {
                throw;
            }
        }

        public RelatorioDespesa GetDespesa(int id)
        {
            try
            {
                RelatorioDespesa despesa = _context.RelatorioDespesas.Find(id);
                return despesa;
            }
            catch { throw; }
           
        }

        public IEnumerable<RelatorioDespesa> GetFiltraDespesas(string criterio)
        {
            List<RelatorioDespesa> desp = new List<RelatorioDespesa>();
            try
            {
                desp = GetAllDespesas().ToList();
                return desp.Where(x => x.ItemNome.IndexOf(criterio,StringComparison.OrdinalIgnoreCase) != -1);
            }
            catch
            {
                throw;
            }
        }

        public int UpdateDespesa(RelatorioDespesa despesa)
        {
            try
            {
                _context.Entry(despesa).State = EntityState.Modified;
                _context.SaveChanges();
                return 1;
            }
            catch { throw; }
        }
    }
}

using MinhasFinancas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasFinancas.DAL
{
    public interface IFinancasDAL
    {
        IEnumerable<RelatorioDespesa> GetAllDespesas();
        IEnumerable<RelatorioDespesa> GetFiltraDespesas(string criterio);
        void AddDespesas(RelatorioDespesa despesa);
        int UpdateDespesa(RelatorioDespesa despesa);
        RelatorioDespesa GetDespesa(int id);
        void DeleteDespesa(int id);
        Dictionary<string , decimal> CalculaDespesaPeriodo(int periodo);
        Dictionary<string , decimal> CalculaDespesaPeriodoSemanal(int periodo);
    }
}

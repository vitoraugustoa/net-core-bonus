using Microsoft.EntityFrameworkCore;
using MinhasFinancas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasFinancas.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {}

        public DbSet<RelatorioDespesa> RelatorioDespesas { get; set; }
    }
}

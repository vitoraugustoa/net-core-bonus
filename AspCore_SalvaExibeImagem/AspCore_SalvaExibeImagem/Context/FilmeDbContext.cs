using AspCore_SalvaExibeImagem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCore_SalvaExibeImagem.Context
{
    public class FilmeDbContext : DbContext
    {
        public FilmeDbContext(DbContextOptions<FilmeDbContext> options): base(options)
        {
        }

        public DbSet<Filme> Filmes { get; set; }
    }
}

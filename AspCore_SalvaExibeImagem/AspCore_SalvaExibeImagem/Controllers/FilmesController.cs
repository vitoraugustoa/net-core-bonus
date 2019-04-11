using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspCore_SalvaExibeImagem.Context;
using AspCore_SalvaExibeImagem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspCore_SalvaExibeImagem.Controllers
{
    public class FilmesController : Controller
    {
        private readonly FilmeDbContext _context;

        public FilmesController(FilmeDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var filmes = _context.Filmes.ToList();
            return View(filmes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Filme filmes , List<IFormFile> Imagem) // recebendo as informações do filme e a imagem
        {
            foreach (var item in Imagem)
            {
                if (item.Length > 0) 
                {
                    using (MemoryStream stream = new MemoryStream()) 
                    {
                        await item.CopyToAsync(stream); // CopyToAsync Lê de forma assincrona os bytes do fluxo atual e os grava em outro fluxo
                        filmes.Imagem = stream.ToArray();
                    }
                }
            }
            _context.Filmes.Add(filmes);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public FileStreamResult ExibirImagem(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(f => f.FilmeId == id);

            if(filme == null)
            {
                return null;
            }

            MemoryStream ms = new MemoryStream(filme.Imagem);
            return new FileStreamResult(ms , "image/jpeg");
        }
    }
}
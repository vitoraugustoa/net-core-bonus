using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspUpload_Download.Context;
using AspUpload_Download.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace AspUpload_Download.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _environment;


        public UsuarioController(AppDbContext context , IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Senha,Email")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            // Obter informações da imagem do usuário do servidor
            string webRoot = _environment.WebRootPath;
            string imagemProcessada = "";
            string nomeDoArquivo = "";

            if (Directory.Exists($"{webRoot}/ArquivosUsuario/{usuario.Id.ToString()}/Imagem/"))
            {
                string[] arquivos = Directory.GetFiles($"{webRoot}/ArquivosUsuario/{usuario.Id.ToString()}/Imagem/" , "*.*");

                if (arquivos.Length > 0)
                {
                    foreach (var arquivo in arquivos)
                    {
                        nomeDoArquivo = Path.GetFileName(arquivo);

                        string _arquivoAtual = arquivo.ToString();
                        if (System.IO.File.Exists(_arquivoAtual))
                        {
                            string _tempArquivoUrl = $"/ArquivosUsuario/{usuario.Id.ToString()}/Imagem/{Path.GetFileName(_arquivoAtual)}";
                            imagemProcessada = _tempArquivoUrl;
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(imagemProcessada))
            {
                ViewBag.CaminhoImagem = Convert.ToString(imagemProcessada);
                ViewBag.NomeArquivo = Convert.ToString(nomeDoArquivo);
            }
            else
            {
                ViewBag.CaminhoImagem = "/image/semimagem.jpg";
            }

            return View("Edit" , usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id , [Bind("Id,Nome,Senha,Email")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        //upload
        [HttpPost]
        public async Task<IActionResult> UploadImagem(IFormCollection form)
        {


            if (form.Files == null || form.Files[0].Length == 0)
            {
                return RedirectToAction("Edit" , new { id = Convert.ToString(form["Id"]) });
            }

            var webRoot = _environment.WebRootPath;
            string usuarioId = Convert.ToString(form["Id"]);
            string nomeUsuario = Convert.ToString(form["Nome"]);
            // Se não existir cria a pasta ArquivosUsuario
            if (!Directory.Exists($"{webRoot}/ArquivosUsuario/"))
            {
                Directory.CreateDirectory($"{webRoot}/ArquivosUsuario/");
            }

            // Se não existir cria as pastas: Id do usuario/Imagem
            if (!Directory.Exists($"{webRoot}/ArquivosUsuario/{usuarioId}/Imagem/"))
            {
                Directory.CreateDirectory($"{webRoot}/ArquivosUsuario/{usuarioId}/Imagem/");
            }

            // Deleta arquivos existentes primeiro e então adiciona novo arquivo
            DeletarArquivos(usuarioId);
            string extensaoArquivo = Path.GetExtension(form.Files[0].FileName);

            var path = Path.Combine(Directory.GetCurrentDirectory() , $"wwwroot/ArquivosUsuario/{usuarioId}/Imagem/" , $"{nomeUsuario}{extensaoArquivo}");

            using (FileStream stream = new FileStream(path , FileMode.Create))
            {
                await form.Files[0].CopyToAsync(stream);
            }

            return RedirectToAction("Edit" , new { id = Convert.ToString(form["Id"]) });
        }

        //Download da imagem
        public async Task<IActionResult> Download(string img , string usuarioId)
        {
            string _nomeDoArquivo = img;
            if (_nomeDoArquivo == null)
                return Content("Nome do arquivo não encontrado");

            string path = Path.Combine(Directory.GetCurrentDirectory() , $"wwwroot/ArquivosUsuario/{usuarioId}/Imagem/" , _nomeDoArquivo);

            var memory = new MemoryStream();
            using (FileStream stream = new FileStream(path , FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory , GetContentType(path) , Path.GetFileName(path));
        }


        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            //Remove os arquivos do usuario

            //Deleta a pasta do usuario
            string dirPath = Path.Combine(Directory.GetCurrentDirectory() , $"wwwroot/ArquivosUsuario/{Convert.ToString(id)}/");

            Directory.Delete(dirPath , true);

            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }

        // Deletar os arquivos do usuario
        private void DeletarArquivos(string usuarioId)
        {
            string dirPath = $"/ArquivosUsuario/{usuarioId}/Imagem/";
            var webRoot = _environment.WebRootPath;

            if (Directory.Exists(webRoot + dirPath))
            {
                string[] arquivos = Directory.GetFiles(webRoot + dirPath);
                if (arquivos.Length > 0)
                {
                    foreach (var arquivo in arquivos)
                    {
                        string _arquivoAtual = arquivo.ToString();
                        if (System.IO.File.Exists(_arquivoAtual))
                        {
                            try
                            {
                                System.IO.File.Delete(_arquivoAtual);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                }
            }
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string , string> GetMimeTypes()
        {
            return new Dictionary<string , string>
           {
               {".png", "image/png" },
               {".jpg", "image/jpg" },
               {".jpeg", "image/jpeg" },
               {".gif", "image/gif" },
               {".jfif" , "image/jfif" }
           };
        }
    }
}

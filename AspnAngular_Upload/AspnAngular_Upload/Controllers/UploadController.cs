using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;

namespace AspnAngular_Upload.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        public UploadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                string nomePastaServidor = "Arquivos";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string caminhoPastaServidor = Path.Combine(webRootPath, nomePastaServidor);
                if (!Directory.Exists(caminhoPastaServidor))
                {
                    Directory.CreateDirectory(caminhoPastaServidor);
                }
                if (file.Length > 0)
                {
                    string nomeArquivo = 
                        ContentDispositionHeaderValue.Parse(file.ContentDisposition)
                        .FileName.Trim('"');
                    string caminhoCompleto = Path.Combine(caminhoPastaServidor, nomeArquivo);
                    using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                return Json("Arquivo enviado com SUCESSO !!!.");
            }
            catch (System.Exception ex)
            {
                return Json("O envio do arquivo FALHOU : " + ex.Message);
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetUploadArquivo.Controllers
{
    public class UploadController : Controller
    {
        IHostingEnvironment _appEnvironment;

        public UploadController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Enviando arquivos pequenos para o servidor Até 30MB
        public async Task<IActionResult> EnviarArquivo(List<IFormFile> arquivos)
        {
            long tamanhoArquivos = arquivos.Sum(f => f.Length);

            // caminho completo do arquivo na localização temporária
            var caminhoArquivo = Path.GetTempFileName();

            // processa o arquivo enviado
            foreach (var arquivo in arquivos)
            {
                if (arquivo == null || arquivo.Length == 0)
                {
                    ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                    return View();
                }

                // < define a pasta >
                string pasta = "arquivos_usuario";
                string milisegundos = DateTime.Now.Millisecond.ToString();
                string extencaoDoArquivo = Path.GetExtension(arquivo.FileName);
                string nomeDoArquivo = $"Usuario_arquivo_{milisegundos}{extencaoDoArquivo}";

                // < obtém o caminho >
                string caminho_WebRoot = _appEnvironment.WebRootPath; // busca o caminho fisico da pasta onde salvar o arquivo
                string caminhoDestinoArquivo = $"{caminho_WebRoot}\\arquivos\\{pasta}\\";
                string caminhoDestinoArquivoOriginal = $"{caminhoDestinoArquivo}\\recebidos\\{nomeDoArquivo}";

                // copiar o arquivo para o local de destino original
                using (FileStream stream = new FileStream(caminhoDestinoArquivoOriginal , FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }
            }

            ViewData["Resultado"] = $"{arquivos.Count} arquivos foram enviados ao servidor, com tamanho total de: {tamanhoArquivos} bytes";
            return View();
        }

    }
}
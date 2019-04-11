using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore_enviaEmail.Services.Interfaces;
using AspNetCore_enviaEmail.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_enviaEmail.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailSender _emailSender;
        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EnviarEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EnviarEmail(EmailViewModel email)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _emailSender.SendEmailAsync(email.Destino , email.Assunto , email.Mensagem);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    return RedirectToAction(nameof(EmailFalha));
                }
            }
            return View(email);
        }

        [HttpGet]
        public IActionResult EmailFalha()
        {
            return View();
        }

        
    }
}
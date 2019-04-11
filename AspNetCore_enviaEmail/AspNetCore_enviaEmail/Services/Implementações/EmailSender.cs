using AspNetCore_enviaEmail.Models;
using AspNetCore_enviaEmail.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AspNetCore_enviaEmail.Services.Implementações
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value; // Obtendo as informaçõs contidas no arquivo appsettings.json
        }

        public Task SendEmailAsync(string email , string subject , string message)
        {
            try
            {
                Execute(email , subject , message).Wait();
                return Task.FromResult(0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Execute(string email , string subject , string message)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(email) ? _emailSettings.ToEmail : email;

                // As instâncias da classe MailMessage são usadas para construir
                // mensagens de e-mail que são transmitidas para um servidor SMTP
                // para entregar usando a classe SmtpClient
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UserEmail , "Vitor Augusto")
                };

                // Representa o endereço de um remetente ou destinatário de email
                // Esta classe é usada pelas classes StmpClient e MailMessage
                // para armazenar informações de endereço para mensagens de email
                mail.To.Add(new MailAddress(toEmail));
                mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

                mail.Subject = $"Macoratti .net - {subject}";
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                // Outras opções
                // mail.Attchments.Add(new Attchment(arquivo));

                using (SmtpClient smtp = new SmtpClient(_emailSettings.Hosts.Gmail , _emailSettings.Port)) // SmtpCliente utilizada para enviar e-mail
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UserEmail , _emailSettings.UserPassword); 
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

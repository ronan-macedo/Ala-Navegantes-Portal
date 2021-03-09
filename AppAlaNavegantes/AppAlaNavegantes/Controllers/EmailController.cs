using AppAlaNavegantes.Models;
using AppAlaNavegantes.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppAlaNavegantes.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender, IWebHostEnvironment env) => _emailSender = emailSender;

        [HttpGet]
        public IActionResult Contato() => View();

        /// <summary>
        /// Inicia o processo de envio após o usuário clicar enviar, recebe os paramêtros
        /// enviados pelo usuário pelo EmailModel.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Envia o e-mail</returns>
        [HttpPost]
        public IActionResult Contato(EmailModel email)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SendEmail(email.Name, email.Subject, email.Message).GetAwaiter();
                    return RedirectToAction("EmailEnviado");
                }
                catch (Exception)
                {
                    return RedirectToAction("EmailFalhou");
                }
            }
            return View(email);
        }

        /// <summary>
        /// Passa os paramêtros do usuários para o processo de autenticação
        /// antes de enviar o e-mail.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns>Autoriza o envio</returns>
        public async Task SendEmail(string name, string subject, string message)
        {
            try
            {
                await _emailSender.EmailSenderAsync(name, subject, message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult EmailFalhou() => View();

        [HttpGet]
        public IActionResult EmailEnviado() => View();
    }
}

using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AppAlaNavegantes.Services
{
    /// <summary>
    /// Implementa a interface IEmailSender com todos os parametros que serão enviados.
    /// </summary>
    public class AuthMessageSender : IEmailSender
    {
        // Busca as configurações de e-mail no appsenttings.json.
        public EmailSettings _emailSettings { get; }

        public AuthMessageSender(IOptions<EmailSettings> emailSettings) => _emailSettings = emailSettings.Value;

        /// <summary>
        /// Inicia o processo de envio do e-mail, apenas o enviado após o retorno da
        /// função Execute.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns>Autoriza o envio do e-mail.</returns>
        public Task EmailSenderAsync(string name, string subject, string message)
        {
            try
            {
                Execute(name, subject, message).Wait();
                return Task.FromResult(0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Monta o email para o envio utilizando todas as informações do _emailSettings
        /// e também dos paramêtros fornecidos pelo usuário.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task Execute(string name, string subject, string message)
        {
            try
            {                
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "Ala Navegantes I")
                };

                mail.To.Add(new MailAddress(_emailSettings.ToEmail));

                mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

                mail.Subject = $"[Portal] - {subject}";

                // Possibilita a utilização de tags HTML no corpo do e-mail.
                mail.IsBodyHtml = true;

                // Monta o corpo do e-mail.
                string content = $"Nome: {name}, ";
                content += $"<br />Mensagem: {message}";

                mail.Body = content;

                // Faz a verificação de usuário para o envio do e-mail. 
                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    // Apenas envia e-mail por meio de usuário e senha pré-definidos.
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
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

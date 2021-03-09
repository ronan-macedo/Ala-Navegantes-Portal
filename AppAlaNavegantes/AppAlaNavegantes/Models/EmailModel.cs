using System.ComponentModel.DataAnnotations;

namespace AppAlaNavegantes.Models
{
    public class EmailModel
    {
        [Required, Display(Name = "Nome"), StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }
        [Required, Display(Name = "Assunto"), StringLength(60, MinimumLength = 3)]
        public string Subject { get; set; }
        [Required, Display(Name = "Mensagem"), StringLength(60, MinimumLength = 3)]
        public string Message { get; set; }
    }
}

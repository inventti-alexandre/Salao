using System.ComponentModel.DataAnnotations;

namespace Salao.Web.Areas.Empresa.Models
{
    public class LoginUsuario
    {
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
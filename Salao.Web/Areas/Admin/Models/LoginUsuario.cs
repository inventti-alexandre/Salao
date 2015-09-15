using System.ComponentModel.DataAnnotations;

namespace Salao.Web.Areas.Admin.Models
{
    public class LoginUsuario
    {
        [Required]
        [Display(Name="Login")]
        public string Login { get; set; }

        [Required]
        [Display(Name="Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
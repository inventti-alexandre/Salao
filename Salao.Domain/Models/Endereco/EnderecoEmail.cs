using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Salao.Domain.Models.Endereco
{
    public class EnderecoEmail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(100, ErrorMessage = "O endereço de e-mail é composto por no máximo 100 caracteres")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        [Range(1,999999999999,ErrorMessage="Endereço inválido")]
        public int IdEndereco { get; set; }


    }
}

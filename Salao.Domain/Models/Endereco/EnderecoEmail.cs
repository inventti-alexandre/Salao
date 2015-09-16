using Salao.Domain.Models.Admin;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Salao.Domain.Models.Endereco
{
    public class EnderecoEmail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Bairro")]
        [StringLength(100, ErrorMessage = "O endereço de e-mail é composto por no máximo 100 caracteres")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool Ativo { get; set; }

        [Required]
        public int AlteradoPor { get; set; }

        [Required]
        public DateTime AlteradoEm { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        [Range(1,999999999999,ErrorMessage="Endereço inválido")]
        public int IdEndereco { get; set; }

        [NotMapped]
        [Display(Name = "Usuario")]
        public virtual Usuario Usuario
        {
            get
            {
                return new Salao.Domain.Service.Admin.UsuarioService().Find(AlteradoPor);
            }
        }

    }
}

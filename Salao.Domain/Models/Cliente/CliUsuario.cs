using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Salao.Domain.Models.Cliente
{
    public class CliUsuario
    {
        [Key]
        public int Id { get; set; }

        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        [StringLength(100, ErrorMessage = "O nome do usuário é composto por no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o e-mail")]
        [StringLength(100, ErrorMessage = "O e-mail do usuário é composto por no máximo 100 caracteres")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha do usuário")]
        [StringLength(20, ErrorMessage = "A senha é composta por no mínimo 6 caracteres e no máximo 20 caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name="Senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Informe o telefone do usuário")]
        [StringLength(20, ErrorMessage = "O telefone é composto por no mínio 6 caracteres e no máximo 20 caracteres", MinimumLength = 6)]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [Display(Name = "Criado em")]
        public DateTime CadastradoEm { get; set; }

        [NotMapped]
        public virtual Empresa Empresa
        {
            get
            {
                return new Service.Cliente.EmpresaService().Find(IdEmpresa);
            }
        }
    }
}

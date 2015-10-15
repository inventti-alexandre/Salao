using System;
using System.ComponentModel.DataAnnotations;

namespace Salao.Domain.Models.Admin
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        [StringLength(100, ErrorMessage = "O nome do usuário é composto por no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o e-mail")]
        [StringLength(100, ErrorMessage = "O e-mail do usuário é composto por no máximo 100 caracteres")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o login")]
        [StringLength(20, ErrorMessage = "O login é composto por no mínimo 4 caracteres e no máximo 20 caracteres", MinimumLength = 4)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe a senha do usuário")]
        [StringLength(20, ErrorMessage = "A senha é composta por no mínimo 6 caracteres e no máximo 20 caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage="Informe o telefone do usuário")]
        [StringLength(20, ErrorMessage="O telefone é composto por no mínio 6 caracteres e no máximo 20 caracteres", MinimumLength=6)]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [StringLength(6, ErrorMessage="O ramal é composto por no máximo 6 caracteres")]
        public string Ramal { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [Display(Name = "Criado em")]
        public DateTime CadastradoEm { get; set; }

        [Display(Name = "Excluído em")]
        public DateTime? ExcluidoEm { get; set; }

        public string Roles { get; set; }
    }
}

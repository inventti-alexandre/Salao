using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Salao.Domain.Models.Cliente
{
    public class CliGrupo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do grupo")]
        [StringLength(40, ErrorMessage = "O nome do grupo é formado por no máximo 40 caracteres")]
        [Display(Name = "Grupo")]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        [Display(Name = "Alterado em")]
        public DateTime AlteradoEm { get; set; }
    }
}

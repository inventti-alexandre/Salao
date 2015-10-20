using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Salao.Domain.Models.Cliente
{
    public class CliPermissao
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a permissão")]
        [StringLength(100, ErrorMessage = "A permissão é composta por no máximo 100 caracteres")]
        [Display(Name = "Permissão")]
        public string Descricao { get; set; }

        [Required(ErrorMessage="Informe a regra")]
        [Display(Name="Regra")]
        [StringLength(40,ErrorMessage="A regra é composta por no máximo 40 caracteres")]
        public string Role { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [Display(Name = "Alterado em")]
        public DateTime AlteradoEm { get; set; }
    }
}

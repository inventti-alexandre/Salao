using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Salao.Domain.Models.Endereco
{
    public class EnderecoTelefone
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(3,ErrorMessage="O DDD é composto por no máximo 3 caracteres")]
        public string DDD { get; set; }

        [Required]
        [StringLength(20, ErrorMessage="O telefone é composto por no máximo 20 caracteres")]
        public string Telefone { get; set; }

        [StringLength(6, ErrorMessage="O ramal é composto por no máximo 6 caracteres")]
        public string Ramal { get; set; }

        [StringLength(40, ErrorMessage="O contato é composto por no máxim 40 caracteres")]
        public string Contato { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [HiddenInput(DisplayValue=false)]
        [Range(1, 999999999999, ErrorMessage = "Endereço inválido")]
        public int IdEndereco { get; set; }
    }
}

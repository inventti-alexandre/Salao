using System.ComponentModel.DataAnnotations;

namespace Salao.Domain.Models.Endereco
{
    public class EnderecoEstado
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Informe o nome do Estado")]
        [Display(Name="Estado")]
        [StringLength(40, ErrorMessage="O nome do estado é composto por no máximo 40 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe a sigla do Estado")]
        [StringLength(2, ErrorMessage = "A UF é composta por 2 caracteres", MinimumLength=2)]
        public string UF { get; set; }

        public bool Ativo { get; set; }
    }
}

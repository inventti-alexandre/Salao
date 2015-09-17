using System.ComponentModel.DataAnnotations;

namespace Salao.Domain.Models.Endereco
{
    public class EnderecoTipoEndereco
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Informe o tipo de endereço")]
        [StringLength(20, ErrorMessage="O tipo de endereço é composto por no máximo 20 caracteres")]
        [Display(Name="Tipo de endereço")]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }        
    }
}

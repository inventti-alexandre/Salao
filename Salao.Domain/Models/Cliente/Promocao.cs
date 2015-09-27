
using System.ComponentModel.DataAnnotations;
namespace Salao.Domain.Models.Cliente
{
    public class Promocao
    {
        [Required]
        [Display(Name="Desconto %")]
        [Range(0,100,ErrorMessage="O desconto varia entre 0% e 100%")]
        public decimal Desconto { get; set; }

        [Required]
        [Display(Name="Carência em meses")]
        [Range(0,12,ErrorMessage="A carência varia entre 0 e 12 meses")]
        public int DescontoCarencia { get; set; }
    }
}

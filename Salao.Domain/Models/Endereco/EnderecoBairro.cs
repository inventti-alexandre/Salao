using Salao.Domain.Service.Endereco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Salao.Domain.Models.Endereco
{
    public class EnderecoBairro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Bairro")]
        [StringLength(100, ErrorMessage="O nome do bairro é composto por no máximo 100 caracteres")]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public int IdCidade { get; set; }

        [NotMapped]
        [Display(Name = "Cidade")]
        public virtual EnderecoCidade Cidade
        {
            get
            {
                return new CidadeService().Find(IdCidade);
            }
        }
    }
}

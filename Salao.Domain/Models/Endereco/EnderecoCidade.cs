using Salao.Domain.Service.Endereco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Salao.Domain.Models.Endereco
{
    public class EnderecoCidade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name="Cidade")]
        [StringLength(100, ErrorMessage="O nome da cidade é composto por no máximo 100 caracteres")]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        [HiddenInput(DisplayValue=false)]
        public int IdEstado { get; set; }

        [NotMapped]
        [Display(Name = "Estado")]
        public virtual EnderecoEstado Estado
        {
            get
            {
                return new EstadoService().Find(IdEstado);
            }
        }
    }
}

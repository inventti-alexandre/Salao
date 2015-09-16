using Salao.Domain.Models.Admin;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        
        [Required]
        public int AlteradoPor { get; set; }

        [Required]
        public DateTime AlteradoEm { get; set; }

        [NotMapped]
        [Display(Name = "Usuario")]
        public virtual Usuario Usuario
        {
            get
            {
                return new Salao.Domain.Service.Admin.UsuarioService().Find(AlteradoPor);
            }
            set { }
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Salao.Domain.Models.Admin
{
    public class SistemaParametro
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Informe o código do parâmetro")]
        [Display(Name = "Código")]
        [StringLength(100, ErrorMessage = "O código do parâmetro é composto por no máximo 100 caracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage="Informe o valor do parâmetro")]
        [StringLength(255, ErrorMessage="O valor do parâmetro é composto por no máximo 255 caracteres")]
        public string Valor { get; set; }

        [Required(ErrorMessage = "Informe a descrição do parâmetro")]
        [Display(Name="Descrição")]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [Display(Name="Alterado por")]
        public int AlteradoPor { get; set; }

        [Required]
        [Display(Name="Alterado em")]
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

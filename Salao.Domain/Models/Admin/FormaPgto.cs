using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salao.Domain.Models.Admin
{
    public class FormaPgto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a forma de pagamento")]
        [StringLength(40, ErrorMessage = "A forma de pagamento é formada por no máximo 40 caracteres")]
        [Display(Name = "Forma de pagamento")]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }
        
        [Required]
        [Display(Name = "Alterado por")]
        public int AlteradoPor { get; set; }

        [Required]
        [Display(Name = "Alterado em")]
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

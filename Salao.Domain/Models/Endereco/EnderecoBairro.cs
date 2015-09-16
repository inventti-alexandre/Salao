using Salao.Domain.Models.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public int AlteradoPor { get; set; }

        [Required]
        public DateTime AlteradoEm { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public int IdCidade { get; set; }

        [NotMapped]
        [Display(Name = "Usuario")]
        public virtual Usuario Usuario
        {
            get
            {
                return new Salao.Domain.Service.Admin.UsuarioService().Find(AlteradoPor);
            }
        }

        [NotMapped]
        [Display(Name = "Cidade")]
        public virtual EnderecoCidade Cidade
        {
            get
            {
                // TODO: ServiceCidade com retorno da cidade
                throw new NotImplementedException();
            }
        }
    }
}

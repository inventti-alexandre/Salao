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
    public class EnderecoCidade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name="Cidade")]
        [StringLength(100, ErrorMessage="O nome da cidade é composto por no máximo 100 caracteres")]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        [Required]
        public int AlteradoPor { get; set; }

        [Required]
        public DateTime AlteradoEm { get; set; }

        [HiddenInput(DisplayValue=false)]
        public int IdEstado { get; set; }

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
        [Display(Name = "Estado")]
        public virtual EnderecoEstado Estado
        {
            get
            {
                // TODO: ServiceEstado com retorno do estado aqui
                throw new NotImplementedException();
            }
        }
    }
}

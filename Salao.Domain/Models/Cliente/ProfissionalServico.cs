using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Salao.Domain.Models.Cliente
{
    public class ProfissionalServico
    {
        [Key]
        public int IdProfissional { get; set; }

        [Key]
        public int IdServico { get; set; }

        [NotMapped]
        [Display(Name="Serviço")]
        public virtual Servico Servico
        {
            get
            {
                return new Service.Cliente.ServicoService().Find(IdServico);
            }
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Salao.Domain.Models.Cliente
{
    public class ProfissionalServico
    {
        [Key]
        public int IdProfissional { get; set; }

        [Key]
        public int IdServico { get; set; }
    }
}

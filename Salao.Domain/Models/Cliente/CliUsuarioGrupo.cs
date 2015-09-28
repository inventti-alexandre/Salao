using System.ComponentModel.DataAnnotations;

namespace Salao.Domain.Models.Cliente
{
    public class CliUsuarioGrupo
    {
        [Key]
        public int IdUsuario { get; set; }

        [Key]
        public int IdGrupo { get; set; }
    }
}

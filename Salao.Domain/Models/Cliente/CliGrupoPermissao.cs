using System.ComponentModel.DataAnnotations;

namespace Salao.Domain.Models.Cliente
{
    public class CliGrupoPermissao
    {
        [Key]
        public int IdGrupo { get; set; }

        [Key]
        public int IdPermissao { get; set; }
    }
}

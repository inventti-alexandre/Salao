using System.ComponentModel.DataAnnotations;

namespace Salao.Domain.Models.Admin
{
    public class GrupoPermissao
    {
        [Key]
        public int IdGrupo { get; set; }

        [Key]
        public int IdPermissao { get; set; }
    }
}

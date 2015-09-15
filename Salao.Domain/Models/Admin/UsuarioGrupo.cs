using System.ComponentModel.DataAnnotations;

namespace Salao.Domain.Models.Admin
{
    public class UsuarioGrupo
    {
        [Key]
        [Required]
        public int IdUsuario { get; set; }

        [Key]
        [Required]
        public int IdGrupo { get; set; }
    }
}

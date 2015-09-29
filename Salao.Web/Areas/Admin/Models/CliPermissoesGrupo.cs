using System.ComponentModel.DataAnnotations;

namespace Salao.Web.Areas.Admin.Models
{
    public class CliPermissoesGrupo
    {
        public int Id { get; set; }

        [Display(Name = "Permissão")]
        public string Descricao { get; set; }

        public bool Selecionado { get; set; }
    }
}
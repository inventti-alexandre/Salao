
using System.ComponentModel.DataAnnotations;
namespace Salao.Web.Areas.Empresa.Models
{
    public class ProfissionalServicoModel
    {
        public int IdProfissional { get; set; }
        public int IdServico { get; set; }

        [Display(Name="Serviço")]
        public string ServicoNome { get; set; }

        [Display(Name="Área")]
        public string Area { get; set; }

        [Display(Name="Sub área")]
        public string SubArea { get; set; }

        public bool Selecionado { get; set; }
    }
}
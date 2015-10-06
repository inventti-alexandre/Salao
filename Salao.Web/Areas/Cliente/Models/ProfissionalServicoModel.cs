
namespace Salao.Web.Areas.Cliente.Models
{
    public class ProfissionalServicoModel
    {
        public int IdProfissional { get; set; }
        public int IdServico { get; set; }
        public string ServicoNome { get; set; }
        public bool Selecionado { get; set; }
    }
}
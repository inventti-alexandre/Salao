using System.ComponentModel.DataAnnotations;

namespace Salao.Web.Areas.Empresa.Models
{
    public class SalaoFormasPagamento
    {
        public int Id { get; set; }

        [Display(Name="Forma de Pagamento")]
        public string Descricao { get; set; }

        public bool Selecionado { get; set; }
    }
}
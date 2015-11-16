using Salao.Domain.Service.Cliente;
using Salao.Web.Areas.Empresa.Common;
using System.Linq;
using System.Web.Mvc;

namespace Salao.Web.Common
{
    public static class BindSalaoHelper
    {
        public static MvcHtmlString SelectSalao(this HtmlHelper html, int idSalao = 0)
        {
            var saloes = new SalaoService().Listar()
                .Where(x => x.IdEmpresa == Identification.IdEmpresa && x.Ativo == true)
                .OrderBy(x => x.Fantasia)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "Saloes");
            tag.MergeAttribute("name", "Saloes");

            foreach (var item in saloes)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idSalao)
                {
                    itemTag.MergeAttribute("selected", "selected");
                }
                itemTag.SetInnerText(item.Fantasia);
                tag.InnerHtml += itemTag.ToString();
            }

            return new MvcHtmlString(tag.ToString());
        }
    }
}
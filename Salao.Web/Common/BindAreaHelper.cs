using Salao.Domain.Service.Admin;
using System.Linq;
using System.Web.Mvc;

namespace Salao.Web.Common
{
    public static class BindAreaHelper
    {
        public static MvcHtmlString SelectArea(this HtmlHelper html, int idArea = 0, string ctrlName="Areas")
        {
            var areas = new AreaService().Listar()
                .Where(x => x.Ativo == true)
                .OrderBy(x => x.Descricao)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", ctrlName);
            tag.MergeAttribute("name", ctrlName);

            foreach (var item in areas)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idArea)
                {
                    itemTag.MergeAttribute("selected", "selected");
                }
                itemTag.SetInnerText(item.Descricao);
                tag.InnerHtml += itemTag.ToString();
            }

            return new MvcHtmlString(tag.ToString());
        }
    }
}
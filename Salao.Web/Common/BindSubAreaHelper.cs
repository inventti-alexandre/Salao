using Salao.Domain.Service.Admin;
using System.Linq;
using System.Web.Mvc;

namespace Salao.Web.Common
{
    public static class BindSubAreaHelper
    {
        public static MvcHtmlString SelectSubArea(this HtmlHelper html, int idSubArea = 0, string ctrlName = "SubAreas")
        {
            var service = new SubAreaService();
            
            var subArea = service.Find(idSubArea);
            if (subArea == null)
            {
                return new MvcHtmlString(string.Empty);
            }

            var subAreas = service.Listar()
                .Where(x => x.Ativo == true && x.IdArea == subArea.IdArea)
                .OrderBy(x => x.Descricao)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", ctrlName);
            tag.MergeAttribute("name", ctrlName);

            foreach (var item in subAreas)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idSubArea)
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
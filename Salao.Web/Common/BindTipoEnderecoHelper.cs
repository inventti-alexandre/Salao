using Salao.Domain.Service.Endereco;
using System.Linq;
using System.Web.Mvc;

namespace Salao.Web.Common
{
    public static class BindTipoEnderecoHelper
    {
        public static MvcHtmlString SelectTipoEndereco(this HtmlHelper html, int idTipoEndereco = 0)
        {
            var tipos = new TipoEnderecoService().Listar()
                .Where(x => x.Ativo == true)
                .OrderBy(x => x.Descricao)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "TipoEndereco");
            tag.MergeAttribute("name", "TipoEndereco");

            foreach (var item in tipos)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idTipoEndereco)
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
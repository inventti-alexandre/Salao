using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Salao.Web.Common
{
    public static class BindTipoPessoaHelper
    {
        public static MvcHtmlString SelectTipoPessoa(this HtmlHelper html, int idTipoPessoa)
        {
            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "TipoPessoa");
            tag.MergeAttribute("name", "TipoPessoa");

            TagBuilder tagFisica = new TagBuilder("option");
            tagFisica.MergeAttribute("value", "1");
            if (idTipoPessoa == 1)
            {
                tagFisica.MergeAttribute("selected", "selected");
            }
            tagFisica.SetInnerText("FÍSICA");
            tag.InnerHtml += tagFisica.ToString();

            TagBuilder tagJuridica = new TagBuilder("option");
            tagJuridica.MergeAttribute("value", "2");
            if (idTipoPessoa == 2)
            {
                tagJuridica.MergeAttribute("selected", "selected");
            }
            tagJuridica.SetInnerText("JURÍDICA");
            tag.InnerHtml += tagJuridica.ToString();

            return new MvcHtmlString(tag.ToString());
        }
    }
}
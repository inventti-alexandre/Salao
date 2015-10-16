using Salao.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Salao.Web.Areas.Empresa.Controllers
{
    [AreaAuthorize("Empresa", Roles = "empresa_master,empresa_mananger,empresa_staff")]
    public class HomeController : Controller
    {
        // GET: Empresa/Home
        public ActionResult Index(string mensagem = "")
        {
            ViewBag.Mensagem = mensagem;
            return View();
        }
    }
}
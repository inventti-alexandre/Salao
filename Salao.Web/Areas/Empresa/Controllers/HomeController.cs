using Salao.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Salao.Web.Areas.Empresa.Controllers
{
    [AreaAuthorizeAttribute("Empresa", Roles = "admin,client")]
    public class HomeController : Controller
    {
        // GET: Empresa/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeAdmController : Controller
    {
        //
        // GET: /Admin/HomeAdm/
        public ActionResult Index(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }
	}
}
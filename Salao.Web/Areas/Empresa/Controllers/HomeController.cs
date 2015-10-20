using Salao.Web.Common;
using System.Web.Mvc;

namespace Salao.Web.Areas.Empresa.Controllers
{
    [AreaAuthorize("Empresa", Roles = "empresa")]
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
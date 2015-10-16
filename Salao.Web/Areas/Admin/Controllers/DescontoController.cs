using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using Salao.Web.Common;

namespace Salao.Web.Areas.Admin.Controllers
{
    [AreaAuthorizeAttribute("Admin", Roles="admin")]
    public class DescontoController : Controller
    {
        PromocaoService service;

        public DescontoController()
        {
            service = new PromocaoService();
        }

        // GET: Admin/Desconto
        public ActionResult Index(string mensagem = "")
        {
            @ViewBag.Mensagem = mensagem;
            return View(service.Get());
        }

        // GET: Admin/Desconto
        [HttpPost]
        public ActionResult Index([Bind(Include="Desconto,DescontoCarencia")] Promocao promocao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Set(promocao);
                    return RedirectToAction("Index", new { mensagem = "Desconto gravado" });
                }
                return View(promocao);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(promocao);
            }
        }
    }
}
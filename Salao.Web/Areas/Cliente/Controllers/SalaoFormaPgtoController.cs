using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Service.Admin;
using Salao.Web.Areas.Cliente.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Salao.Domain.Service.Cliente;

namespace Salao.Web.Areas.Cliente.Controllers
{
    public class SalaoFormaPgtoController : Controller
    {
        ISalaoFormaPgto service;

        public SalaoFormaPgtoController ()
	    {
            service = new SalaoFormaPgtoService();
	    }
            
        //
        // GET: /Cliente/SalaoFormaPgto/
        public ActionResult Index(int? id)
        {
            if (id == null)
	        {
		        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	        }

            // salao
            var salao = new Domain.Service.Cliente.SalaoService().Find((int)id);

            // formas de pagamento
            var formas = new FormaPgtoService().Listar().Where(x => x.Ativo == true).OrderBy(x => x.Descricao).ToList();

            var model = new List<SalaoFormaPgtoModel>();

            foreach (var item in formas)
            {
                model.Add(new SalaoFormaPgtoModel
                {
                    FomaPgto = item.Descricao,
                    IdFormaPgto = item.Id,
                    IdSalao = (int)id,
                    Selecionado = (service.Listar().Where(x => x.IdSalao == id && x.IdFormaPgto == item.Id).Count() > 0)
                });
            }

            ViewBag.Fantasia = salao.Fantasia;
            ViewBag.IdSalao = salao.Id;
            ViewBag.IdEmpresa = salao.IdEmpresa;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int[] selecionado, int idSalao, int IdEmpresa)
        {
            // grava formas de pagamento do salao
            service.Gravar(idSalao, selecionado);

            return RedirectToAction("Index", "Salao", new { idEmpresa = IdEmpresa });
        }
    }
}

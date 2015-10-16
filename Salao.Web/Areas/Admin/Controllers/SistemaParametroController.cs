using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Admin;
using Salao.Domain.Models.Admin;
using Salao.Domain.Service.Admin;
using Salao.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [AreaAuthorizeAttribute("Admin", Roles="admin")]
    public class SistemaParametroController : Controller
    {
        private IBaseService<SistemaParametro> service;
        private ILogin login;

        public SistemaParametroController()
        {
            service = new SistemaParametroService();
            login = new UsuarioService();
        }

        //
        // GET: /Admin/SistemaParametro/
        public ActionResult Index()
        {
            var parametros = service.Listar().OrderBy(x => x.Codigo);

            return View(parametros);
        }

        //
        // GET: /Admin/SistemaParametro/Details/5
        public ActionResult Details(int id)
        {
            var parametro = service.Find(id);

            if (parametro == null)
            {
                return HttpNotFound();
            }

            return View(parametro);
        }

        //
        // GET: /Admin/SistemaParametro/Create
        public ActionResult Create()
        {
            return View(new SistemaParametro());
        }

        //
        // POST: /Admin/SistemaParametro/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="Codigo,Valor,Descricao")] SistemaParametro parametro)
        {
            try
            {
                parametro.AlteradoEm = DateTime.Now;
                parametro.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                TryUpdateModel(parametro);

                if (ModelState.IsValid)
	            {
		            service.Gravar(parametro);
                    return RedirectToAction("Index");
	            }

                return View(parametro);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(parametro);
            }
        }

        //
        // GET: /Admin/SistemaParametro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var parametro = service.Find((int)id);

            if (parametro == null)
            {
                return HttpNotFound();
            }

            return View(parametro);
        }

        //
        // POST: /Admin/SistemaParametro/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include="Id,Codigo,Valor,Descricao")] SistemaParametro parametro)
        {
            try
            {
                parametro.AlteradoEm = DateTime.Now;
                parametro.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                TryUpdateModel(parametro);

                if (ModelState.IsValid)
                {
                    service.Gravar(parametro);
                    return RedirectToAction("Index");                    
                }

                return View(parametro);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(parametro);
            }
        }

        //
        // GET: /Admin/SistemaParametro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var parametro = service.Find((int)id);

            if (parametro == null)
            {
                return HttpNotFound();
            }

            return View(parametro);
        }

        //
        // POST: /Admin/SistemaParametro/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch
            {
                var parametro = service.Find(id);
                if (parametro == null)
                {
                    return HttpNotFound();
                }
                return View(parametro);
            }
        }
    }
}

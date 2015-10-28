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
        private IBaseService<SistemaParametro> _service;
        private ILogin _login;

        public SistemaParametroController(IBaseService<SistemaParametro> service, ILogin login)
        {
            _service = service;
            _login = login;
        }

        //
        // GET: /Admin/SistemaParametro/
        public ActionResult Index()
        {
            var parametros = _service.Listar().OrderBy(x => x.Codigo);

            return View(parametros);
        }

        //
        // GET: /Admin/SistemaParametro/Details/5
        public ActionResult Details(int id)
        {
            var parametro = _service.Find(id);

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
                parametro.AlteradoPor = _login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                TryUpdateModel(parametro);

                if (ModelState.IsValid)
	            {
		            _service.Gravar(parametro);
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

            var parametro = _service.Find((int)id);

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
                parametro.AlteradoPor = _login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                TryUpdateModel(parametro);

                if (ModelState.IsValid)
                {
                    _service.Gravar(parametro);
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

            var parametro = _service.Find((int)id);

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
                _service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch
            {
                var parametro = _service.Find(id);
                if (parametro == null)
                {
                    return HttpNotFound();
                }
                return View(parametro);
            }
        }
    }
}

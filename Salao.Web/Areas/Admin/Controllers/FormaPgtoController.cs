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
    public class FormaPgtoController : Controller
    {
        private IBaseService<FormaPgto> _service;
        private ILogin _login;

        public FormaPgtoController(IBaseService<FormaPgto> service, ILogin login)
        {
            _service = service;
            _login = login;
        }

        //
        // GET: /Admin/FormaPgto/
        public ActionResult Index()
        {
            var formas = _service.Listar()
                .OrderBy(x => x.Descricao);

            return View(formas);
        }

        //
        // GET: /Admin/FormaPgto/Details/5
        public ActionResult Details(int id)
        {
            var forma = _service.Find(id);

            if (forma == null)
            {
                return HttpNotFound();
            }

            return View(forma);
        }

        //
        // GET: /Admin/FormaPgto/Create
        public ActionResult Create()
        {
            return View(new FormaPgto());
        }

        //
        // POST: /Admin/FormaPgto/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="Descricao")] FormaPgto forma)
        {
            try
            {
                forma.AlteradoPor = _login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                forma.AlteradoEm = DateTime.Now;
                TryUpdateModel(forma);

                if (ModelState.IsValid)
                {
                    _service.Gravar(forma);
                    return RedirectToAction("Index");                    
                }

                return View(forma);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(forma);
            }
        }

        //
        // GET: /Admin/FormaPgto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var forma = _service.Find((int)id);

            if (forma == null)
            {
                return HttpNotFound();
            }

            return View(forma);
        }

        //
        // POST: /Admin/FormaPgto/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Descricao,Ativo")] FormaPgto forma)
        {
            try
            {
                forma.AlteradoPor = _login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                forma.AlteradoEm = DateTime.Now;
                TryUpdateModel(forma);

                _service.Gravar(forma);
                return RedirectToAction("Index");
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(forma);
            }
        }

        //
        // GET: /Admin/FormaPgto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var forma = _service.Find((int)id);

            if (forma == null)
            {
                return HttpNotFound();
            }

            return View(forma);
        }

        //
        // POST: /Admin/FormaPgto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var forma = _service.Find(id);
                if (forma == null)
                {
                    return HttpNotFound();
                }
                return View(forma);
            }
        }
    }
}

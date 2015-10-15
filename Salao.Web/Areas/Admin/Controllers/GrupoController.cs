using Salao.Domain.Abstract;
using Salao.Domain.Models.Admin;
using Salao.Domain.Service.Admin;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [AreaAuthorizeAttribute("Admin", Roles="admin")]
    public class GrupoController : Controller
    {
        private IBaseService<Grupo> service;

        public GrupoController()
        {
            service = new GrupoService();
        }

        //
        // GET: /Admin/Grupo/
        public ActionResult Index()
        {
            var grupos = service.Listar()
                .OrderBy(x => x.Descricao);

            return View(grupos);
        }

        //
        // GET: /Admin/Grupo/Details/5
        public ActionResult Details(int id)
        {
            var grupo = service.Find(id);

            if (grupo == null)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }

        //
        // GET: /Admin/Grupo/Create
        public ActionResult Create()
        {
            return View(new Grupo());
        }

        //
        // POST: /Admin/Grupo/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Descricao")] Grupo grupo)
        {
            try
            {
                grupo.AlteradoEm = DateTime.Now;
                TryUpdateModel(grupo);

                if (ModelState.IsValid)
                {
                    service.Gravar(grupo);
                    return RedirectToAction("Index");
                }

                return View(grupo);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(grupo);
            }
        }

        //
        // GET: /Admin/Grupo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var grupo = service.Find((int)id);

            if (grupo == null)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }

        //
        // POST: /Admin/Grupo/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Descricao,Ativo")] Grupo grupo)
        {
            try
            {
                grupo.AlteradoEm = DateTime.Now;
                TryUpdateModel(grupo);

                if (ModelState.IsValid)
                {
                    service.Gravar(grupo);
                    return RedirectToAction("Index");
                }

                return View(grupo);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(grupo);
            }
        }

        //
        // GET: /Admin/Grupo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var grupo = service.Find((int)id);

            if (grupo == null)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }

        //
        // POST: /Admin/Grupo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                var grupo = service.Find(id);
                if (grupo == null)
                {
                    return HttpNotFound();
                }
                ModelState.AddModelError(string.Empty, e.Message);
                return View(grupo);
            }
        }
    }
}

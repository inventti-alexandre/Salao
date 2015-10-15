using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Admin;
using Salao.Domain.Models.Admin;
using Salao.Domain.Service.Admin;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [AreaAuthorizeAttribute("Admin", Roles="admin")]
    public class AreaController : Controller
    {
        private IBaseService<Area> service;
        private ILogin login;

        public AreaController()
        {
            service = new AreaService();
            login = new UsuarioService();
        }

        //
        // GET: /Admin/Area/
        public ActionResult Index()
        {
            var areas = service.Listar()
                .OrderBy(x => x.Descricao);

            return View(areas);
        }

        //
        // GET: /Admin/Area/Details/5
        public ActionResult Details(int id)
        {
            var area = service.Find(id);

            if (area == null)
            {
                return HttpNotFound();
            }

            return View(area);
        }

        //
        // GET: /Admin/Area/Create
        public ActionResult Create()
        {
            return View(new Area());
        }

        //
        // POST: /Admin/Area/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="Descricao")] Area area)
        {
            try
            {
                area.AlteradoEm = DateTime.Now;
                area.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                TryUpdateModel(area);

                if (ModelState.IsValid)
                {
                    service.Gravar(area);
                    return RedirectToAction("Index");
                }

                return View(area);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(area);
            }
        }

        //
        // GET: /Admin/Area/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var area = service.Find((int)id);

            if (area == null)
            {
                return HttpNotFound();
            }

            return View(area);
        }

        //
        // POST: /Admin/Area/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include="Id,Descricao,Ativo")] Area area)
        {
            try
            {
                area.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                area.AlteradoEm = DateTime.Now;
                TryUpdateModel(area);

                if (ModelState.IsValid)
                {
                    service.Gravar(area);
                    return RedirectToAction("Index");
                }

                return View(area);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(area);
            }
        }

        //
        // GET: /Admin/Area/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var area = service.Find((int)id);

            if (area == null)
            {
                return HttpNotFound();
            }

            return View(area);
        }

        //
        // POST: /Admin/Area/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var area = service.Find(id);
                if (area == null)
                {
                    return HttpNotFound();
                }
                return View(area);
            }
        }
    }
}

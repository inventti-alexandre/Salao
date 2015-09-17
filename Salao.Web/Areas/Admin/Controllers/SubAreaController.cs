using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Admin;
using Salao.Domain.Models.Admin;
using Salao.Domain.Service.Admin;
using System.Net;

namespace Salao.Web.Areas.Admin.Controllers
{
    public class SubAreaController : Controller
    {
        private IBaseService<SubArea> service;
        private IBaseService<Area> serviceArea;

        private ILogin login;

        public SubAreaController()
        {
            service = new SubAreaService();
            serviceArea = new AreaService();
            login = new UsuarioService();
        }

        //
        // GET: /Admin/SubArea/
        public ActionResult Index(int idArea)
        {
            // area
            var area = serviceArea.Find(idArea);

            // subareas cadastradas
            var subAreas = service.Listar()
                .Where(x => x.IdArea == idArea)
                .OrderBy(x => x.Descricao);

            ViewBag.IdArea = idArea;
            ViewBag.NomeArea = area.Descricao;

            return View(subAreas);
        }

        //
        // GET: /Admin/SubArea/Details/5
        public ActionResult Details(int id)
        {
            var subArea = service.Find(id);

            if (subArea == null)
            {
                return HttpNotFound();
            }

            return View(subArea);
        }

        //
        // GET: /Admin/SubArea/Create
        public ActionResult Create(int IdArea)
        {
            var area = serviceArea.Find(IdArea);
            var subArea = new SubArea { IdArea = area.Id };
            ViewBag.Area = area.Descricao;
            return View(subArea);
        }

        //
        // POST: /Admin/SubArea/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="Descricao,IdArea")] SubArea subArea)
        {
            try
            {
                subArea.AlteradoEm = DateTime.Now;
                subArea.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                TryUpdateModel(subArea);
                service.Gravar(subArea);

                return RedirectToAction("Index", new { idArea = subArea.IdArea });
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.NomeArea = serviceArea.Find(subArea.IdArea).Descricao;
                return View(subArea);
            }
        }

        //
        // GET: /Admin/SubArea/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var subArea = service.Find((int)id);
            
            if (subArea == null)
            {
                return HttpNotFound();
            }

            return View(subArea);
        }

        //
        // POST: /Admin/SubArea/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include="Id,Descricao,Ativo,IdArea")] SubArea subArea)
        {
            try
            {
                subArea.AlteradoEm = DateTime.Now;
                subArea.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                TryUpdateModel(subArea);
                return RedirectToAction("Index", new { idArea = subArea.IdArea });
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(subArea);
            }
        }

        //
        // GET: /Admin/SubArea/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var subArea = service.Find((int)id);
            
            if (subArea == null)
            {
                return HttpNotFound();
            }

            return View(subArea);
        }

        //
        // POST: /Admin/SubArea/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var subArea = service.Excluir(id);
                return RedirectToAction("Index", new { idArea = subArea.IdArea });
            }
            catch
            {
                var subArea = service.Find(id);
                if (subArea == null)
                {
                    return HttpNotFound();
                }
                return View(subArea);
            }
        }
    }
}

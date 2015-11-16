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
    public class SubAreaController : Controller
    {
        private IBaseService<SubArea> _service;
        private IBaseService<Area> _serviceArea;
        private ILogin _login;

        public SubAreaController(IBaseService<SubArea> service, IBaseService<Area> serviceArea, ILogin login)
        {
            _service = service;
            _serviceArea = serviceArea;
            _login = login;
        }

        //
        // GET: /Admin/SubArea/
        public ActionResult Index(int idArea)
        {
            // area
            var area = _serviceArea.Find(idArea);

            // subareas cadastradas
            var subAreas = _service.Listar()
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
            var subArea = _service.Find(id);

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
            var area = _serviceArea.Find(IdArea);
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
                subArea.AlteradoPor = _login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                TryUpdateModel(subArea);
                _service.Gravar(subArea);

                return RedirectToAction("Index", new { idArea = subArea.IdArea });
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.NomeArea = _serviceArea.Find(subArea.IdArea).Descricao;
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

            var subArea = _service.Find((int)id);
            
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
                subArea.AlteradoPor = _login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
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

            var subArea = _service.Find((int)id);
            
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
                var subArea = _service.Excluir(id);
                return RedirectToAction("Index", new { idArea = subArea.IdArea });
            }
            catch
            {
                var subArea = _service.Find(id);
                if (subArea == null)
                {
                    return HttpNotFound();
                }
                return View(subArea);
            }
        }
        //
        // GET: /Admin/SubArea/GetSubAreas
        public JsonResult GetSubAreas(int idArea)
        {
            if (HttpContext.Request.IsAjaxRequest())
            {
                IQueryable subs = _service.Listar()
                    .Where(x => x.IdArea == idArea)
                    .OrderBy(x => x.Descricao);

                return Json(new SelectList(subs, "Id", "Descricao"), JsonRequestBehavior.AllowGet);
            }

            return null;
        }
    }
}

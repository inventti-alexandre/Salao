using Salao.Domain.Abstract;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using Salao.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [AreaAuthorizeAttribute("Admin", Roles="admin")]
    public class CliGrupoController : Controller
    {
        private IBaseService<CliGrupo> service;

        public CliGrupoController()
        {
            service = new CliGrupoService();
        }

        // GET: Admin/CliGrupo
        public ActionResult Index(int? idEmpresa)
        {
            if (idEmpresa == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var empresa = new EmpresaService().Find((int)idEmpresa);

            if (empresa == null)
            {
                return HttpNotFound();
            }

            var grupos = service.Listar()
                .Where(x => x.IdEmpresa == (int)idEmpresa)
                .OrderBy(x => x.Descricao);

            ViewBag.Fantasia = empresa.Fantasia;
            ViewBag.IdEmpresa = empresa.Id;
            return View(grupos);
        }

        // GET: Admin/CliGrupo/Details/5
        public ActionResult Details(int id)
        {
            var grupo = service.Find(id);

            if (grupo == null)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }

        // GET: Admin/CliGrupo/Create
        public ActionResult Create(int? idEmpresa)
        {
            if (idEmpresa == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var empresa = new EmpresaService().Find((int)idEmpresa);

            if (empresa == null)
            {
                return HttpNotFound();
            }

            return View(new CliGrupo { IdEmpresa = empresa.Id });
        }

        // POST: Admin/CliGrupo/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="IdEmpresa,Descricao")] CliGrupo grupo)
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
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(grupo);
            }
        }

        // GET: Admin/CliGrupo/Edit/5
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

        // POST: Admin/CliGrupo/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include="Id,IdEmpresa,Descricao,Ativo")] CliGrupo grupo)
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
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(grupo);
            }
        }

        // GET: Admin/CliGrupo/Delete/5
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

        // POST: Admin/CliGrupo/Delete/5
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

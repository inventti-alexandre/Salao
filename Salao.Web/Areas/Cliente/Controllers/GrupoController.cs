using Salao.Domain.Abstract;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Cliente.Controllers
{
    [Authorize]
    public class GrupoController : Controller
    {
        private IBaseService<CliGrupo> service;

        public GrupoController()
        {
            service = new CliGrupoService();
        }

        // GET: Cliente/Grupo
        public ActionResult Index(int idEmpresa)
        {
            var grupos = service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa)
                .OrderBy(x => x.Descricao);

            ViewBag.IdEmpresa = idEmpresa;
            return View(grupos);
        }

        // GET: Cliente/Grupo/Details/5
        public ActionResult Details(int id)
        {
            var grupo = service.Find(id);

            if (grupo == null)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }

        // GET: Cliente/Grupo/Create
        public ActionResult Create(int idEmpresa)
        {
            return View(new CliGrupo { IdEmpresa = idEmpresa});
        }

        // POST: Cliente/Grupo/Create
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
                    return RedirectToAction("Index", new { idEmpresa = grupo.IdEmpresa });
                }

                return View(grupo);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(grupo);
            }
        }

        // GET: Cliente/Grupo/Edit/5
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

        // POST: Cliente/Grupo/Edit/5
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
                    return RedirectToAction("Index", new { idEmpresa = grupo.IdEmpresa });
                }
                return View(grupo);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(grupo);
            }
        }

        // GET: Cliente/Grupo/Delete/5
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

        // POST: Cliente/Grupo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var grupo = service.Excluir(id);
                return RedirectToAction("Index", new { idEmpresa = grupo.IdEmpresa });
            }
            catch
            {
                var grupo = service.Find(id);
                if (grupo == null)
                {
                    return HttpNotFound();
                }
                return View(grupo);
            }
        }
    }
}

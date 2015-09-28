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
        public ActionResult Index()
        {
            var grupos = service.Listar()
                .OrderBy(x => x.Descricao);

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
        public ActionResult Create()
        {
            return View(new CliGrupo());
        }

        // POST: Cliente/Grupo/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="Descricao")] CliGrupo grupo)
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
        public ActionResult Edit([Bind(Include="Id,Descricao,Ativo")] CliGrupo grupo)
        {
            try
            {
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
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index");
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

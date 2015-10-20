using Salao.Domain.Abstract;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using Salao.Web.Areas.Empresa.Common;
using Salao.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Empresa.Controllers
{
    [AreaAuthorize("Empresa", Roles = "grupo_crud")]
    public class GrupoController : Controller
    {
        private IBaseService<CliGrupo> service;

        public GrupoController()
        {
            service = new CliGrupoService();
        }

        // GET: Empresa/Grupo
        public ActionResult Index()
        {
            var grupos = service.Listar()
                .Where(x => x.IdEmpresa == Identification.IdEmpresa)
                .OrderBy(x => x.Descricao)
                .ToList();

            return View(grupos);
        }

        // GET: Empresa/Grupo/Details/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var grupo = service.Find((int)id);

            if (grupo == null || grupo.IdEmpresa != Identification.IdEmpresa)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }

        // GET: Empresa/Grupo/Create
        public ActionResult Incluir()
        {
            return View(new CliGrupo { IdEmpresa = Identification.IdEmpresa });
        }

        // POST: Empresa/Grupo/Create
        [HttpPost]
        public ActionResult Incluir([Bind(Include="IdEmpresa,Descricao")] CliGrupo grupo)
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

        // GET: Empresa/Grupo/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var grupo = service.Find((int)id);

            if (grupo == null || grupo.IdEmpresa != Identification.IdEmpresa)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }

        // POST: Empresa/Grupo/Edit/5
        [HttpPost]
        public ActionResult Editar([Bind(Include="Id,IdEmpresa,Descricao,Ativo")] CliGrupo grupo)
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

        // GET: Empresa/Grupo/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var grupo = service.Find((int)id);

            if (grupo == null || grupo.IdEmpresa != Identification.IdEmpresa)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }

        // POST: Empresa/Grupo/Delete/5
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                var grupo = service.Find(id);
                if (grupo == null || grupo.IdEmpresa != Identification.IdEmpresa)
                {
                    return HttpNotFound();
                }
                ModelState.AddModelError(string.Empty, e.Message);
                return View(grupo);
            }
        }
    }
}

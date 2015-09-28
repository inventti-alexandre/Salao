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
    public class PermissaoController : Controller
    {
        private IBaseService<CliPermissao> service;

        public PermissaoController()
        {
            service = new CliPermissaoService();
        }

        //
        // GET: /Cliente/Permissao/
        public ActionResult Index(int idEmpresa)
        {
            var permissoes = service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa)
                .ToList();

            ViewBag.IdEmpresa = idEmpresa;
            return View(permissoes);
        }

        //
        // GET: /Cliente/Permissao/Details/5
        public ActionResult Details(int id)
        {
            var permissao = service.Find(id);

            if (permissao == null)
            {
                return HttpNotFound();
            }

            return View(permissao);
        }

        //
        // GET: /Cliente/Permissao/Create
        public ActionResult Create(int idEmpresa)
        {
            return View(new CliPermissao { IdEmpresa = idEmpresa });
        }

        //
        // POST: /Cliente/Permissao/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="IdEmpresa,Descricao")] CliPermissao permissao)
        {
            try
            {
                permissao.AlteradoEm = DateTime.Now;
                TryUpdateModel(permissao);

                if (ModelState.IsValid)
                {
                    service.Gravar(permissao);
                    return RedirectToAction("Index", new { idEmpresa = permissao.IdEmpresa });
                }

                return View(permissao);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(permissao);
            }
        }

        //
        // GET: /Cliente/Permissao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var permissao = service.Find((int)id);

            if (permissao == null)
            {
                return HttpNotFound();
            }

            return View(permissao);
        }

        //
        // POST: /Cliente/Permissao/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include="Id,IdEmpresa,Descricao,Ativo")] CliPermissao permissao)
        {
            try
            {
                permissao.AlteradoEm = DateTime.Now;
                TryUpdateModel(permissao);

                if (ModelState.IsValid)
                {
                    service.Gravar(permissao);
                    return RedirectToAction("Index", new { idEmpresa = permissao.IdEmpresa });
                }

                return View(permissao);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(permissao);
            }
        }

        //
        // GET: /Cliente/Permissao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var permissao = service.Find((int)id);

            if (permissao == null)
            {
                return HttpNotFound();
            }

            return View(permissao);
        }

        //
        // POST: /Cliente/Permissao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var permissao = service.Excluir(id);
                return RedirectToAction("Index", new { idEmpresa = permissao.IdEmpresa});
            }
            catch
            {
                var permissao = service.Find(id);
                if (permissao == null)
                {
                    return HttpNotFound();
                }
                return View(permissao);
            }
        }
    }
}

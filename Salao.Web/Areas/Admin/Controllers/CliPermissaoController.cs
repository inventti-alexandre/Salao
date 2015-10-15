using Salao.Domain.Abstract;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [AreaAuthorizeAttribute("Admin", Roles="admin")]
    public class CliPermissaoController : Controller
    {
        private IBaseService<CliPermissao> service;

        public CliPermissaoController()
        {
            service = new CliPermissaoService();
        }

        // GET: Admin/CliPermissao
        public ActionResult Index()
        {
            var permissoes = service.Listar()
                .OrderBy(x => x.Descricao);

            return View(permissoes);
        }

        // GET: Admin/CliPermissao/Details/5
        public ActionResult Details(int id)
        {
            var permissao = service.Find(id);

            if (permissao == null)
            {
                return HttpNotFound();
            }

            return View(permissao);
        }

        // GET: Admin/CliPermissao/Create
        public ActionResult Create()
        {
            return View(new CliPermissao());
        }

        // POST: Admin/CliPermissao/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="Descricao")] CliPermissao permissao)
        {
            try
            {
                permissao.AlteradoEm = DateTime.Now;
                TryUpdateModel(permissao);

                if (ModelState.IsValid)
                {
                    service.Gravar(permissao);
                    return RedirectToAction("Index");
                }
                return View(permissao);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(permissao);
            }
        }

        // GET: Admin/CliPermissao/Edit/5
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

        // POST: Admin/CliPermissao/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include="Id,Descricao,Ativo")] CliPermissao permissao)
        {
            try
            {
                permissao.AlteradoEm = DateTime.Now;
                TryUpdateModel(permissao);

                if (ModelState.IsValid)
                {
                    service.Gravar(permissao);
                    return RedirectToAction("Index");
                }
                return View(service);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(permissao);
            }
        }

        // GET: Admin/CliPermissao/Delete/5
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

        // POST: Admin/CliPermissao/Delete/5
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
                var permissao = service.Find(id);
                if (permissao == null)
                {
                    return HttpNotFound();
                }
                ModelState.AddModelError(string.Empty, e.Message);
                return View(permissao);
            }
        }
    }
}

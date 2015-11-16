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
    public class PermissaoController : Controller
    {
        private IBaseService<Permissao> _service;
        private ILogin _login;

        public PermissaoController(IBaseService<Permissao> service, ILogin login)
        {
            _service = service;
            _login = login;
        }

        //
        // GET: /Admin/Permissao/
        public ActionResult Index()
        {
            var permissoes = _service.Listar()
                .OrderBy(x => x.Descricao);

            return View(permissoes);
        }

        //
        // GET: /Admin/Permissao/Details/5
        public ActionResult Details(int id)
        {
            var permissao = _service.Find(id);

            if (permissao == null)
            {
                return HttpNotFound();
            }

            return View(permissao);
        }

        //
        // GET: /Admin/Permissao/Create
        public ActionResult Create()
        {
            return View(new Permissao());
        }

        //
        // POST: /Admin/Permissao/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="Descricao")] Permissao permissao)
        {
            try
            {
                permissao.AlteradoPor = _login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                permissao.AlteradoEm = DateTime.Now;
                TryUpdateModel(permissao);

                if (ModelState.IsValid)
                {
                    _service.Gravar(permissao);
                    return RedirectToAction("Index");
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
        // GET: /Admin/Permissao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var permissao = _service.Find((int)id);

            if (permissao == null)
            {
                return HttpNotFound();
            }

            return View(permissao);
        }

        //
        // POST: /Admin/Permissao/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include="Id,Descricao,Ativo")] Permissao permissao)
        {
            try
            {
                permissao.AlteradoPor = _login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                TryUpdateModel(permissao);

                if (ModelState.IsValid)
                {
                    _service.Gravar(permissao);
                    return RedirectToAction("Index");
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
        // GET: /Admin/Permissao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var permissao = _service.Find((int)id);

            if (permissao == null)
            {
                return HttpNotFound();
            }

            return View(permissao);
        }

        //
        // POST: /Admin/Permissao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch
            {
                var permissao = _service.Find(id);
                if (permissao == null)
                {
                    return HttpNotFound();
                }
                return View(permissao);
            }
        }
    }
}

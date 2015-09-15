using Salao.Domain.Abstract;
using Salao.Domain.Models.Admin;
using Salao.Domain.Service.Admin;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {

        private IBaseService<Usuario> service;

        public UsuarioController()
        {
            service = new UsuarioService();
        }

        // GET: /Admin/Usuario/
        public ActionResult Index()
        {
            var usuarios = service.Listar().Where(x => x.Ativo == true)
                .OrderBy(x => x.Nome);

            return View(usuarios);
        }

        //
        // GET: /Admin/Usuario/Details/5
        public ActionResult Details(int id)
        {
            var usuario = service.Find(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        //
        // GET: /Admin/Usuario/Create
        public ActionResult Create()
        {            
            return View(new Usuario());
        }

        //
        // POST: /Admin/Usuario/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="Nome,Email,Login,Senha,Telefone,Ramal")] Usuario usuario)
        {
            try
            {
                usuario.CadastradoEm = DateTime.Now;
                TryUpdateModel(usuario);

                if (ModelState.IsValid)
                {
                    service.Gravar(usuario);
                    return RedirectToAction("Index");
                }
                
                return View(usuario);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(usuario);
            }
        }

        //
        // GET: /Admin/Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = service.Find((int)id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            usuario.Senha = string.Empty;
            return View(usuario);
        }

        //
        // POST: /Admin/Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include="Id,Nome,Email,Login,Senha,Ativo,CadastradoEm,ExcluidoEm,Telefone,Ramal")] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Gravar(usuario);
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(usuario);
            }
        }

        //
        // GET: /Admin/Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = service.Find((int)id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        //
        // POST: /Admin/Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch
            {
                var usuario = service.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
        }
    }
}

using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Salao.Web.Areas.Cliente.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        IBaseService<CliUsuario> service;
        ILogin login;

        public UsuarioController()
        {
            service = new CliUsuarioService();
            login = new CliUsuarioService();
        }

        // GET: Cliente/Usuario
        public ActionResult Index(int? idEmpresa)
        {
            if (idEmpresa == null)
            {
                return HttpNotFound();
            }

            var usuarios = service.Listar().Where(x => x.IdEmpresa == idEmpresa);
            ViewBag.IdEmpresa = idEmpresa;
            return View(usuarios);
        }

        // GET: Cliente/Usuario/Details/5
        public ActionResult Details(int? id)
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

        // GET: Cliente/Usuario/Create
        public ActionResult Create(int idEmpresa)
        {
            var usuario = new CliUsuario { IdEmpresa = idEmpresa };
            usuario.Password = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
            return View(usuario);
        }

        // POST: Cliente/Usuario/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="IdEmpresa,Nome,Email,Password,Telefone,Password")] CliUsuario usuario)
        {
            try
            {
                usuario.CadastradoEm = DateTime.Now;
                TryUpdateModel(usuario);

                if (ModelState.IsValid)
                {
                    service.Gravar(usuario);
                    return RedirectToAction("Index", new { idEmpresa = usuario.IdEmpresa });
                }

                return View(usuario);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(usuario);
            }
        }

        // GET: Cliente/Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = service.Find((int)id);
            usuario.Password = string.Empty;

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }
        
        // POST: Cliente/Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "IdEmpresa,Nome,Email,Telefone,Password,CadastradoEm")] CliUsuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Gravar(usuario);
                    return RedirectToAction("Index", new { idEmpresa = usuario.IdEmpresa });
                }
                return View(usuario);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(usuario);
            }
        }

        // GET: Cliente/Usuario/Delete/5
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

        // POST: Cliente/Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int idEmpresa)
        {
            try
            {
                if (login.GetIdUsuarioByNome(System.Web.HttpContext.Current.User.Identity.Name.ToUpper().Trim(), idEmpresa) == id)
                {
                    throw new ArgumentException("Não é permitido que o usuário corrente exclusa a si próprio");
                }

                var usuario = service.Excluir(id);
                return RedirectToAction("Index", new { idEmpresa = usuario.IdEmpresa });
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var usuario = service.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
        }

        // POST: Cliente/Usuario/Situacao/5
        public ActionResult Situacao(int? id)
        {
            return View();
        }

        // POST: Cliente/Usuario/Situacao/5
        [HttpPost]
        public ActionResult Situacao(int idUsuario, bool Ativo)
        {
            return View();
        }

    }
}

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
    [AreaAuthorize("Empresa", Roles = "cliusuario_crud")]
    public class UsuarioController : Controller
    {
        IBaseService<CliUsuario> service;

        public UsuarioController()
        {
            service = new CliUsuarioService();
        }

        // GET: Empresa/Usuario
        public ActionResult Index(bool soAtivos = true)
        {
            var usuarios = service.Listar()
                .Where(x => x.IdEmpresa == Identification.IdEmpresa
                && (soAtivos == true || x.Ativo == soAtivos))
                .OrderBy(x => x.Nome);

            return View(usuarios);
        }

        // GET: Empresa/Usuario/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = service.Find((int)id);

            if (usuario == null || usuario.IdEmpresa != Identification.IdEmpresa)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // GET: Empresa/Usuario/Incluir
        public ActionResult Incluir()
        {
            var usuario = new CliUsuario
            {
                IdEmpresa = Identification.IdEmpresa,
                Password = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8)
            };

            return View(usuario);
        }

        // POST: Empresa/Usuario/Incluir
        [HttpPost]
        public ActionResult Incluir([Bind(Include = "IdEmpresa,Nome,Email,Password,Telefone")] CliUsuario usuario)
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
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(usuario);
            }
        }

        // GET: Empresa/Usuario/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = service.Find((int)id);

            if (usuario == null || usuario.IdEmpresa != Identification.IdEmpresa)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // POST: Empresa/Usuario/Edit/5
        [HttpPost]
        public ActionResult Editar([Bind(Include="Id,IdEmpresa,Nome,Email,Ativo,Telefone,CadastradoEm,Password")] CliUsuario usuario)
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
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(usuario);
            }
        }

        // GET: Empresa/Usuario/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = service.Find((int)id);

            if (usuario == null || usuario.IdEmpresa != Identification.IdEmpresa)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // POST: Empresa/Usuario/Delete/5
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            try
            {
                if (id == Identification.IdUsuario)
                {
                    throw new ArgumentException("Não é permitido que o usuário corrente exclusa a si próprio");
                }

                var usuario = service.Excluir(id);

                return RedirectToAction("Index");
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

        // GET: Empresa/Usuario/RedefinirSenha/5
        public ActionResult RedefinirSenha(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = service.Find((int)id);

            if (usuario == null || usuario.IdEmpresa != Identification.IdEmpresa)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // POST: Empresa/Usuario/RedefinirSenha/5
        [HttpPost]
        public ActionResult RedefinirSenha(int id)
        {
            var usuario = service.Find(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            try
            {
                var redefinir = new CliUsuarioService();
                redefinir.RedefinirSenha(usuario.Id);
                return View("SenhaRedefinida", usuario);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(usuario);
            }
        }
    }
}

using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using Salao.Web.Areas.Admin.Models;
using Salao.Web.Common;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [AreaAuthorizeAttribute("Admin", Roles="admin")]
    public class CliUsuarioGrupoController : Controller
    {
        IBaseService<CliUsuario> serviceUsuario;
        IBaseService<CliGrupo> serviceGrupo;
        ICliUsuarioGrupo service;

        public CliUsuarioGrupoController()
        {
            serviceUsuario = new CliUsuarioService();
            serviceGrupo = new CliGrupoService();
            service = new CliUsuarioGrupoService();
        }

        //
        // GET: /Admin/CliUsuarioGrupo/
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // usuario
            var usuario = serviceUsuario.Find((int)id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            // grupos disponiveis
            var grupos = serviceGrupo.Listar().Where(x => x.Ativo == true).OrderBy(x => x.Descricao).ToList();

            // lista retorno
            var gruposUsuario = new List<GruposUsuario>();
            foreach (var item in grupos)
            {
                gruposUsuario.Add(new GruposUsuario
                {
                    Descricao = item.Descricao,
                    Id = item.Id,
                    Selecionado = (service.Listar().Where(x => x.IdGrupo == item.Id && x.IdUsuario == id).Count() > 0)
                });
            }

            ViewBag.IdUsuario = id;
            ViewBag.NomeUsuario = usuario.Nome;
            ViewBag.IdEmpresa = usuario.IdEmpresa;

            return View(gruposUsuario);
        }

        [HttpPost]
        public ActionResult Index(int[] selecionado, int idUsuario, int idEmpresa)
        {
            // grava grupos do usuario
            service.Gravar(idUsuario, selecionado);

            return RedirectToAction("Index", "UsuarioCliente", new { idEmpresa = idEmpresa });
        }
    }
}

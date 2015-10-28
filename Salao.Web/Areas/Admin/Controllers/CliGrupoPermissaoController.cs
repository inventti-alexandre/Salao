using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using Salao.Web.Areas.Admin.Models;
using Salao.Web.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [AreaAuthorizeAttribute("Admin", Roles="admin")]
    public class CliGrupoPermissaoController : Controller
    {
        IBaseService<CliGrupo> _serviceGrupo;
        IBaseService<CliPermissao> _servicePermissao;
        IGrupoPermissao _service;

        public CliGrupoPermissaoController(IBaseService<CliGrupo> serviceGrupo, IBaseService<CliPermissao> servicePermissao, IGrupoPermissao service)
        {
            _serviceGrupo = serviceGrupo;
            _servicePermissao = servicePermissao;
            _service = service;
        }

        // GET: Admin/CliGrupoPermissao
        public ActionResult Index(int id, int idEmpresa)
        {
            // grupo selecionado
            var grupo = _serviceGrupo.Find(id);

            // permissoes disponiveis
            var permissoes = _servicePermissao.Listar().Where(x => x.Ativo == true).OrderBy(x => x.Descricao).ToList();

            // lista retorno
            var permissoesGrupo = new List<CliPermissoesGrupo>();
            foreach (var item in permissoes)
            {
                permissoesGrupo.Add(new CliPermissoesGrupo
                {
                    Descricao = item.Descricao,
                    Id = item.Id,
                    Selecionado = (_service.Listar().Where(x => x.IdPermissao == item.Id && x.IdGrupo == id).Count() > 0)
                });
            }

            ViewBag.IdGrupo = id;
            ViewBag.NomeGrupo = grupo.Descricao;
            ViewBag.IdEmpresa = idEmpresa;
            return View(permissoesGrupo);
        }

        [HttpPost]
        public ActionResult Index(int idGrupo, int[] selecionado, int idEmpresa)
        {
            // grava permissoes do grupo
            _service.Gravar(idGrupo, selecionado);

            return RedirectToAction("Index", "CliGrupo", new { idEmpresa = idEmpresa });
        }
    }
}

using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using Salao.Web.Areas.Admin.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class CliGrupoPermissaoController : Controller
    {
        IBaseService<CliGrupo> serviceGrupo;
        IBaseService<CliPermissao> servicePermissao;
        IGrupoPermissao service;

        public CliGrupoPermissaoController()
        {
            serviceGrupo = new CliGrupoService();
            servicePermissao = new CliPermissaoService();
            service = new CliGrupoPermissaoService();
        }

        // GET: Admin/CliGrupoPermissao
        public ActionResult Index(int id)
        {
            // grupo selecionado
            var grupo = serviceGrupo.Find(id);

            // permissoes disponiveis
            var permissoes = servicePermissao.Listar().Where(x => x.Ativo == true).OrderBy(x => x.Descricao).ToList();

            // lista retorno
            var permissoesGrupo = new List<CliPermissoesGrupo>();
            foreach (var item in permissoes)
            {
                permissoesGrupo.Add(new CliPermissoesGrupo
                {
                    Descricao = item.Descricao,
                    Id = item.Id,
                    Selecionado = (service.Listar().Where(x => x.IdPermissao == item.Id && x.IdGrupo == id).Count() > 0)
                });
            }

            ViewBag.IdGrupo = id;
            ViewBag.NomeGrupo = grupo.Descricao;
            return View(permissoesGrupo);
        }

        [HttpPost]
        public ActionResult Index(int idGrupo, int[] selecionado)
        {
            // grava permissoes do grupo
            service.Gravar(idGrupo, selecionado);

            return RedirectToAction("Index", "CliGrupo");
        }
    }
}

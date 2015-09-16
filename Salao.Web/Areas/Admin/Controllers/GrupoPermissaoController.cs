using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Admin;
using Salao.Domain.Models.Admin;
using Salao.Domain.Service.Admin;
using Salao.Web.Areas.Admin.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class GrupoPermissaoController : Controller
    {

        IBaseService<Grupo> serviceGrupo;
        IBaseService<Permissao> servicePermissao;
        IGrupoPermissao serviceGrupoPermissao;

        public GrupoPermissaoController()
        {
            serviceGrupo = new GrupoService();
            servicePermissao = new PermissaoService();
            serviceGrupoPermissao = new GrupoPermissaoService();
        }

        //
        // GET: /Admin/GrupoPermissao/
        public ActionResult Index(int id)
        {
            // grupo selecionado
            var grupo = serviceGrupo.Find(id);
            
            // permissoes disponiveis
            var permissoes = servicePermissao.Listar().Where(x => x.Ativo == true).OrderBy(x => x.Descricao).ToList();

            // lista retorno
            var permissoesGrupo = new List<PermissoesGrupo>();
            foreach (var item in permissoes)
            {
                permissoesGrupo.Add(new PermissoesGrupo
                {
                    Descricao = item.Descricao,
                    Id = item.Id,
                    Selecionado = (serviceGrupoPermissao.Listar().Where(x => x.IdPermissao == item.Id && x.IdGrupo == id).Count() > 0)
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
            serviceGrupoPermissao.Gravar(idGrupo, selecionado);

            return RedirectToAction("Index", "Grupo");
        }
	}
}
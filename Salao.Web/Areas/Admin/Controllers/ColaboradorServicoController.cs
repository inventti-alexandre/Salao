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
    public class ColaboradorServicoController : Controller
    {
        IProfissionalServico _service;
        IBaseService<Profissional> _serviceProfissional;
        IBaseService<Servico> _serviceServico;

        public ColaboradorServicoController(IProfissionalServico service, IBaseService<Profissional> serviceProfissional, IBaseService<Servico> serviceServico)
        {
            _service = service;
            _serviceProfissional = serviceProfissional;
            _serviceServico = serviceServico;
        }

        // GET: Cliente/ColaboradorServico
        public ActionResult Index(int idProfissional)
        {
            // profissional
            var profissional = _serviceProfissional.Find(idProfissional);

            if (profissional == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // servicos
            var servicos = _serviceServico.Listar()
                .Where(x => x.Ativo == true)
                .OrderBy(x => x.Descricao);

            var model = new List<ProfissionalServicoModel>();

            foreach (var item in servicos)
            {
                model.Add(new ProfissionalServicoModel
                {
                    IdProfissional = idProfissional,
                    IdServico = item.Id,
                    ServicoNome = item.Descricao,
                    Selecionado = (_service.Listar().Where(x => x.IdProfissional == idProfissional && x.IdServico == item.Id).Count() > 0)
                });
            }

            ViewBag.IdProfissional = idProfissional;
            ViewBag.IdSalao = profissional.IdSalao;
            ViewBag.ProfissionalNome = profissional.Nome;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int idProfissional, int[] selecionado, int idSalao)
        {
            // grava servicos deste profissional
            _service.Gravar(idProfissional, selecionado);
            return RedirectToAction("Index", "Colaborador", new { idSalao = idSalao });
        }

    }
}

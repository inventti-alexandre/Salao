using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using Salao.Web.Areas.Admin.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    public class ColaboradorServicoController : Controller
    {
        IProfissionalServico service;
        IBaseService<Profissional> serviceProfissional;
        IBaseService<Servico> serviceServico;

        public ColaboradorServicoController()
        {
            service = new ProfissionalServicoService();
            serviceProfissional = new ProfissionalService();
            serviceServico = new ServicoService();
        }

        // GET: Cliente/ColaboradorServico
        public ActionResult Index(int idProfissional)
        {
            if (idProfissional == null)
            {
                return HttpNotFound();
            }
            
            // profissional
            var profissional = serviceProfissional.Find(idProfissional);

            if (profissional == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // servicos
            var servicos = serviceServico.Listar()
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
                    Selecionado = (service.Listar().Where(x => x.IdProfissional == idProfissional && x.IdServico == item.Id).Count() > 0)
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
            service.Gravar(idProfissional, selecionado);
            return RedirectToAction("Index", "Colaborador", new { idSalao = idSalao });
        }

    }
}

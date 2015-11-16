using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Models.Admin;
using Salao.Domain.Service.Admin;
using Salao.Domain.Service.Cliente;
using Salao.Web.Areas.Empresa.Common;
using Salao.Web.Areas.Empresa.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Empresa.Controllers
{
    public class FormaPagamentoController : Controller
    {
        ISalaoFormaPgto _service;
        IBaseService<FormaPgto> _serviceForma;
        IBaseService<Salao.Domain.Models.Cliente.Salao> _serviceSalao;

        public FormaPagamentoController(ISalaoFormaPgto service, IBaseService<FormaPgto> serviceForma, IBaseService<Salao.Domain.Models.Cliente.Salao> serviceSalao)
        {
            _service = service;
            _serviceForma = serviceForma;
            _serviceSalao = serviceSalao;
        }

        // GET: Empresa/FormaPagamento
        public ActionResult Index(int idSalao = 0)
        {
            if (idSalao == 0)
            {
                var primeiroSalao = _serviceSalao.Listar().FirstOrDefault();
                if (primeiroSalao != null)
                {
                    idSalao = primeiroSalao.Id;
                }
                else
                {
                    return RedirectToAction("Index", "Filial");
                }
            }

            // salao
            var salao = _serviceSalao.Find(idSalao);

            if (salao == null || salao.IdEmpresa != Identification.IdEmpresa)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.IdSalao = idSalao;
            ViewBag.Saloes = GetSaloes(idSalao);
            ViewBag.SalaoFantasia = salao.Fantasia;
            ViewBag.SalaoEndereco = string.Format("{0}, {1}", salao.Endereco.Logradouro, salao.Endereco.Numero);

            return View();
        }

        public PartialViewResult FormasPagamento(int idSalao)
        {
            // salao
            var salao = _serviceSalao.Find(idSalao);

            // formas de pagamento disponiveis
            var formas = _serviceForma.Listar().Where(x => x.Ativo == true).OrderBy(x => x.Descricao).ToList();

            // lista retorno
            var lista = new List<SalaoFormasPagamento>();
            foreach (var item in formas)
            {
                lista.Add(new SalaoFormasPagamento
                {
                    Descricao = item.Descricao,
                    Id = item.Id,
                    Selecionado = (_service.Listar().Where(x => x.IdSalao == idSalao && x.IdFormaPgto == item.Id).Count() > 0)
                });
            }

            ViewBag.IdSalao = idSalao;
            ViewBag.Saloes = GetSaloes(idSalao);
            ViewBag.SalaoFantasia = salao.Fantasia;
            ViewBag.SalaoEndereco = string.Format("{0}, {1}", salao.Endereco.Logradouro, salao.Endereco.Numero);

            return PartialView(lista);
        }

        private SelectList GetSaloes(int idSalao)
        {
            return new SelectList(
                    _serviceSalao.Listar().Where(x => x.IdEmpresa == Identification.IdEmpresa).OrderBy(x => x.Fantasia).ToList(),
                    "Id",
                    "Fantasia",
                    idSalao);
        }        
    }
}
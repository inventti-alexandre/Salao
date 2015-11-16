using Salao.Domain.Abstract;
using Salao.Domain.Models.Admin;
using Salao.Domain.Service.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Salao.Web.Controllers
{
    public class ParceriaController : Controller
    {
        private IBaseService<PreContato> _service;
        private IBaseService<Salao.Domain.Models.Endereco.EnderecoEstado> _serviceEstado;

        public ParceriaController(IBaseService<PreContato> service, IBaseService<Salao.Domain.Models.Endereco.EnderecoEstado> serviceEstado)
	    {
            _service = service;
            _serviceEstado = serviceEstado;
	    }

        //
        // GET: /Parceria/
        public ActionResult Index()
        {
            ViewBag.Estados = GetEstados(null);
            return View(new PreContato());
        }

        // POST: /Parceria/
        [HttpPost]
        public ActionResult Index([Bind(Include="Nome,NomeSalao,Email,Telefone,Cidade,IdEstado")] PreContato contato)
        {
            contato.ContatoEm = DateTime.Now;
            contato.Observ = string.Empty;
            TryUpdateModel(contato);

            if (ModelState.IsValid)
            {
                _service.Gravar(contato);
                return View("_ContatoThanks");
            }

            ViewBag.Estados = GetEstados(contato.IdEstado);
            return View(contato);            
        }

        private List<SelectListItem> GetEstados(int? id)
        {
            var estados = _serviceEstado.Listar().Where(x => x.Ativo == true).OrderBy(x => x.UF).ToList();
            var items = new List<SelectListItem>();
            foreach (var item in estados)
	        {
                items.Add(new SelectListItem { Text = item.UF, Value = item.Id.ToString(), Selected = (item.Id == id) });
	        }
            return items;
        }
	}
}
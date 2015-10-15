using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using Salao.Domain.Service.Endereco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [AreaAuthorizeAttribute("Admin", Roles="admin")]
    public class SalaoController : Controller
    {
        IBaseService<Salao.Domain.Models.Cliente.Salao> service;
        IBaseService<Salao.Domain.Models.Cliente.Empresa> serviceEmpresa;
        ICadastroSalao cadastro;

        public SalaoController()
        {
            service = new Salao.Domain.Service.Cliente.SalaoService();
            serviceEmpresa = new EmpresaService();
            cadastro = new Salao.Domain.Service.Cliente.CadastroSalaoService();
        }

        //
        // GET: /Cliente/Salao/
        public ActionResult Index(int idEmpresa)
        {
            var empresa = serviceEmpresa.Find(idEmpresa);

            if (empresa == null)
            {
                return HttpNotFound();
            }

            var saloes = service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa)
                .OrderBy(x => x.Fantasia);

            ViewBag.Fantasia = empresa.Fantasia;
            ViewBag.IdEmpresa = idEmpresa;

            return View(saloes);
        }

        //
        // GET: /Cliente/Salao/Details/5
        public ActionResult Details(int id)
        {
            var salao = service.Find(id);

            if (salao == null)
            {
                return HttpNotFound();
            }

            return View(salao);
        }

        //
        // GET: /Cliente/Salao/Create
        public ActionResult Create(int idEmpresa)
        {
            // empresa
            var empresa = new EmpresaService().Find(idEmpresa);
            if (empresa == null)
	        {
		        return HttpNotFound();
        	}

            // promocao padrao da empresa
            var promocao = new PromocaoService().Get();

            var model = new CadastroSalao();
            model.Cortesia = true;
            model.Desconto = promocao.Desconto;
            model.DescontoCarencia = promocao.DescontoCarencia;
            model.TipoPessoa = empresa.TipoPessoa;
            model.IdEmpresa = idEmpresa;

            ViewBag.TipoPessoa = GetTipoPessoa(model.TipoPessoa);
            ViewBag.TipoEndereco = GetTipoEndereco();
            ViewBag.IdEstado = GetEstados();
            ViewBag.EmpresaFantasia = empresa.Fantasia ;

            return View(model);
        }

        //
        // POST: /Cliente/Salao/Create
        [HttpPost]
        public ActionResult Create(CadastroSalao model)
        {
            try
            {
                model.AlteradoEm = DateTime.Now;
                model.Aprovado = false;
                model.Ativo = true;
                model.CadastradoEm = DateTime.Now;
                if (ModelState.IsValid)
                {
                    cadastro.Gravar(model);
                    return RedirectToAction("Index", new { idEmpresa = model.IdEmpresa });    
                }

                ViewBag.TipoPessoa = GetTipoPessoa(model.TipoPessoa);
                ViewBag.TipoEndereco = GetTipoEndereco();
                ViewBag.IdEstado = GetEstados();
                ViewBag.EmpresaFantasia = new EmpresaService().Find(model.IdEmpresa);

                return View(model);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.TipoPessoa = GetTipoPessoa(model.TipoPessoa);
                ViewBag.TipoEndereco = GetTipoEndereco();
                ViewBag.IdEstado = GetEstados();
                ViewBag.EmpresaFantasia = new EmpresaService().Find(model.IdEmpresa);

                return View(model);
            }
        }

        //
        // GET: /Cliente/Salao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = cadastro.Find((int)id);

            if (model == null)
            {
                return HttpNotFound();
            }

            ViewBag.TipoPessoa = GetTipoPessoa(model.TipoPessoa);
            ViewBag.TipoEndereco = GetTipoEndereco();
            ViewBag.IdEstado = GetEstados();
            ViewBag.EmpresaFantasia = new EmpresaService().Find(model.IdEmpresa).Fantasia;

            return View(model);
        }

        //
        // POST: /Cliente/Salao/Edit/5
        [HttpPost]
        public ActionResult Edit(CadastroSalao model)
        {
            try
            {
                model.AlteradoEm = DateTime.Now;
                if (ModelState.IsValid)
                {
                    cadastro.Gravar(model);
                    return RedirectToAction("Index", new { idEmpresa = model.IdEmpresa });
                }

                ViewBag.TipoPessoa = GetTipoPessoa(model.TipoPessoa);
                ViewBag.TipoEndereco = GetTipoEndereco();
                ViewBag.IdEstado = GetEstados();
                ViewBag.EmpresaFantasia = new EmpresaService().Find(model.IdEmpresa);

                return View(model);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.TipoPessoa = GetTipoPessoa(model.TipoPessoa);
                ViewBag.TipoEndereco = GetTipoEndereco();
                ViewBag.IdEstado = GetEstados();
                ViewBag.EmpresaFantasia = new EmpresaService().Find(model.IdEmpresa);

                return View(model);
            }
        }

        //
        // GET: /Cliente/Salao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var salao = service.Find((int)id);

            if (salao == null)
            {
                return HttpNotFound();
            }
            
            return View(salao);
        }

        //
        // POST: /Cliente/Salao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int idEmpresa)
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index", new { idEmpresa = idEmpresa });
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var salao = service.Find(id);
                if (salao == null)
                {
                    return HttpNotFound();
                }
                return View(salao);
            }
        }

        #region [ Privates ]

        private List<SelectListItem> GetTipoPessoa(int tipo = 1)
        {
            var tipos = new List<SelectListItem>();
            tipos.Add(new SelectListItem { Text = "FÍSICA", Value = "1", Selected = (tipo == 1) });
            tipos.Add(new SelectListItem { Text = "JURÍDICA", Value = "2", Selected = (tipo == 2) });
            return tipos;
        }

        private List<SelectListItem> GetTipoEndereco(int id = 0)
        {
            var tipos = new TipoEnderecoService().Listar()
                .Where(x => x.Ativo == true).OrderBy(x => x.Descricao);

            var lista = new List<SelectListItem>();
            foreach (var item in tipos)
            {
                lista.Add(new SelectListItem { Text = item.Descricao, Value = item.Id.ToString(), Selected = (item.Id == id) });
            }

            return lista;
        }

        private List<SelectListItem> GetEstados(int id = 0)
        {
            var estados = new EstadoService().Listar()
                .Where(x => x.Ativo == true)
                .OrderBy(x => x.UF);

            var lista = new List<SelectListItem>();
            foreach (var item in estados)
            {
                lista.Add(new SelectListItem { Text = item.UF, Value = item.Id.ToString(), Selected = (item.Id == id) });
            }
            return lista;
        }

        #endregion
    }
}

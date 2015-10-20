using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using Salao.Domain.Service.Endereco;
using Salao.Web.Areas.Empresa.Common;
using Salao.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Empresa.Controllers
{
    [AreaAuthorize("Empresa", Roles = "salao_crud")]
    public class FilialController : Controller
    {
        IBaseService<Salao.Domain.Models.Cliente.Salao> service;
        IBaseService<Salao.Domain.Models.Cliente.Empresa> serviceEmpresa;
        ICadastroSalao cadastro;

        public FilialController()
        {
            service = new SalaoService();
            serviceEmpresa = new EmpresaService();
            cadastro = new CadastroSalaoService();
        }

        // GET: Empresa/Filial
        public ActionResult Index()
        {
            var saloes = service.Listar().Where(x => x.IdEmpresa == Identification.IdEmpresa);
            return View(saloes);
        }

        // GET: Empresa/Filial/Details/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var salao = service.Find((int)id);

            if (salao.IdEmpresa != Identification.IdEmpresa)
            {
                return HttpNotFound();                
            }

            return View(salao);
        }

        // GET: Empresa/Filial/Create
        public ActionResult NovaFilial()
        {
            // empresa
            var empresa = Identification.Empresa;

            // promocao padrao
            var promocao = new PromocaoService().Get();

            var model = new CadastroSalao();
            model.Cortesia = true;
            model.Desconto = promocao.Desconto;
            model.DescontoCarencia = promocao.DescontoCarencia;
            model.TipoPessoa = empresa.TipoPessoa;
            model.IdEmpresa = empresa.Id;

            ViewBag.TipoPessoa = GetTipoPessoa(model.TipoPessoa);
            ViewBag.TipoEndereco = GetTipoEndereco();
            ViewBag.IdEstado = GetEstados();
            ViewBag.EmpresaFantasia = empresa.Fantasia;

            return View(model);
        }

        // POST: Empresa/Filial/Create
        [HttpPost]
        public ActionResult NovaFilial(CadastroSalao model)
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
                    return RedirectToAction("Index");
                }

                throw new Exception();
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.TipoPessoa = GetTipoPessoa(model.TipoPessoa);
                ViewBag.TipoEndereco = GetTipoEndereco();
                ViewBag.IdEstado = GetEstados();
                ViewBag.EmpresaFantasia = Identification.EmpresaFantasia;
                return View(model);
            }
        }

        // GET: Empresa/Filial/Editar/5
        public ActionResult Editar(int? id)
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

            if (model.IdEmpresa != Identification.IdEmpresa)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.TipoPessoa = GetTipoPessoa(model.TipoPessoa);
            ViewBag.TipoEndereco = GetTipoEndereco();
            ViewBag.IdEstado = GetEstados();
            return View(model);
        }

        // POST: Empresa/Filial/Editar/5
        [HttpPost]
        public ActionResult Editar(CadastroSalao model)
        {
            try
            {
                model.AlteradoEm = DateTime.Now;
                if (ModelState.IsValid)
                {
                    cadastro.Gravar(model);
                    return RedirectToAction("Index");
                }

                ViewBag.TipoPessoa = GetTipoPessoa(model.TipoPessoa);
                ViewBag.TipoEndereco = GetTipoEndereco();
                ViewBag.IdEstado = GetEstados();
                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.TipoPessoa = GetTipoPessoa(model.TipoPessoa);
                ViewBag.TipoEndereco = GetTipoEndereco();
                ViewBag.IdEstado = GetEstados();
                return View(model);
            }
        }

        // GET: Empresa/Filial/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var salao = service.Find((int)id);

            if (salao == null || salao.IdEmpresa != Identification.IdEmpresa)
	        {
                return HttpNotFound();
	        }

            return View(salao);
        }

        // POST: Empresa/Filial/Delete/5
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index");
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

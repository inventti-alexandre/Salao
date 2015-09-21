using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Salao.Domain.Abstract;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Models.Endereco;
using Salao.Domain.Service.Endereco;
using Salao.Domain.Service.Cliente;
using Salao.Domain.Abstract.Admin;

namespace Salao.Web.Areas.Admin.Controllers
{
    public class EmpresaController : Controller
    {
        IBaseService<Empresa> serviceEmpresa;
        ICadastroEmpresa serviceCadastro;

        public EmpresaController()
        {
            serviceEmpresa = new EmpresaService();
            serviceCadastro = new CadastroEmpresaService();
        }
        //
        // GET: /Admin/Empresa/
        public ActionResult Index(string fantasia = "")
        {
            fantasia = fantasia.ToUpper().Trim();

            var empresas = serviceEmpresa.Listar()
                .Where(x => fantasia == "" || x.Fantasia.Contains(fantasia))
                .OrderBy(x => x.Fantasia);

            return View(empresas);
        }

        //
        // GET: /Admin/Empresa/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Empresa/Create
        public ActionResult Create()
        {
            // TODO: desconto, descontocarencia -> not hard code            
            var cadastro = new CadastroEmpresa { Desconto = 100, DescontoCarencia = 3, Cortesia = true };

            ViewBag.TipoPessoa = GetTipoPessoa(1);
            ViewBag.TipoEndereco = GetTipoEndereco();
            ViewBag.Estados = GetEstados();

            return View(cadastro);
        }

        //
        // POST: /Admin/Empresa/Create
        [HttpPost]
        public ActionResult Create(CadastroEmpresa cadastro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO - gravar empresa
                    

                    // TODO - inclusao do salao

                    return RedirectToAction("Index");
                }

                ViewBag.TipoPessoa = GetTipoPessoa(1);
                ViewBag.TipoEndereco = GetTipoEndereco();
                ViewBag.Estados = GetEstados();
                return View(cadastro);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.TipoPessoa = GetTipoPessoa(1);
                ViewBag.TipoEndereco = GetTipoEndereco();
                ViewBag.Estados = GetEstados();
                return View(cadastro);
            }
        }

        //
        // GET: /Admin/Empresa/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Empresa/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Empresa/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Empresa/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private List<SelectListItem> GetTipoPessoa(int tipo = 1)
        {
            var tipos = new List<SelectListItem>();
            tipos.Add(new SelectListItem { Text = "FÍSICA", Value = "1", Selected = (tipo == 1) });
            tipos.Add(new SelectListItem { Text = "JURÍDICA", Value = "2", Selected = (tipo == 1) });
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
    }
}

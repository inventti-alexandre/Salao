using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Admin;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Admin;
using Salao.Domain.Service.Cliente;
using Salao.Domain.Service.Endereco;
using Salao.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [AreaAuthorizeAttribute("Admin", Roles="admin")]
    public class EmpresaController : Controller
    {
        IBaseService<Salao.Domain.Models.Cliente.Empresa> serviceEmpresa;
        ICadastroEmpresa serviceCadastro;
        ILogin login;

        public EmpresaController()
        {
            serviceEmpresa = new EmpresaService();
            serviceCadastro = new CadastroEmpresaService();
            login = new UsuarioService();
        }

        // GET: /Admin/Empresa/
        public ActionResult Index(string fantasia = "")
        {
            fantasia = fantasia.ToUpper().Trim();

            var empresas = serviceEmpresa.Listar()
                .Where(x => x.Fantasia.Contains(fantasia))
                .Take(10)
                .OrderBy(x => x.Fantasia);

            return View(empresas);
        }

        // GET: /Admin/Empresa/Details/5
        public ActionResult Details(int id)
        {
            var cadastro = serviceCadastro.Find(id);

            if (cadastro == null)
            {
                return HttpNotFound();
            }

            return View(cadastro);
        }

        // GET: /Admin/Empresa/Create
        public ActionResult Create(int? idPreContato)
        {
            // promocao padrao da empresa
            var promocao = new PromocaoService().Get();

            // novo cadastro
            var cadastro = new CadastroEmpresa { Desconto = promocao.Desconto, DescontoCarencia = promocao.DescontoCarencia, Cortesia = true, TipoPessoa = 2 };    

            // cadastro a partir do pre contato
            if (idPreContato != null)
            {
                var preContato = new PreContatoService().Find((int)idPreContato);
                if (preContato != null)
                {
                    cadastro.Fantasia = preContato.NomeSalao;
                    cadastro.Contato = preContato.Nome;
                    cadastro.Email = preContato.Email;
                    cadastro.Telefone = preContato.Telefone;
                    cadastro.Cidade = preContato.Cidade;
                    cadastro.IdEstado = preContato.IdEstado;                    
                }
            }

            ViewBag.TipoPessoa = GetTipoPessoa(cadastro.TipoPessoa);
            ViewBag.TipoEndereco = GetTipoEndereco();
            ViewBag.Estados = GetEstados();

            return View(cadastro);
        }

        // POST: /Admin/Empresa/Create
        [HttpPost]
        public ActionResult Create(CadastroEmpresa cadastro)
        {
            try
            {
                cadastro.CadastradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                cadastro.Id = 0;
                TryUpdateModel(cadastro);

                if (ModelState.IsValid)
                {
                    serviceCadastro.Gravar(cadastro);
                    // TODO - redirect to inclusao do salao
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

        // GET: /Admin/Empresa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var cadastro = serviceCadastro.Find((int)id);

                if (cadastro == null)
                {
                    return HttpNotFound();
                }

                ViewBag.TipoPessoa = GetTipoPessoa(cadastro.TipoPessoa);
                ViewBag.TipoEndereco = GetTipoEndereco(cadastro.TipoEndereco);
                ViewBag.Estados = GetEstados(cadastro.IdEstado);
                return View(cadastro);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Index");
            }
        }

        // POST: /Admin/Empresa/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,IdEndereco,Fantasia,RazaoSocial,TipoPessoa,Cnpj,Cpf,TipoEndereco,Cep,Logradouro,Numero,Bairro,Cidade,IdEstado,Contato,Email,DDD,Telefone,Observ,Cortesia,Desconto,DescontoCarencia")] CadastroEmpresa cadastro)
        {
            try
            {
                cadastro.CadastradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                if (string.IsNullOrEmpty(cadastro.Cnpj))
                {
                    cadastro.Cnpj = "";
                }
                if (string.IsNullOrEmpty(cadastro.Cpf))
                {
                    cadastro.Cpf = "";   
                }

                if (ModelState.IsValid)
                {
                    serviceCadastro.Gravar(cadastro);
                    return RedirectToAction("Index");
                }

                ViewBag.TipoPessoa = GetTipoPessoa(cadastro.TipoPessoa);
                ViewBag.TipoEndereco = GetTipoEndereco(cadastro.TipoEndereco);
                ViewBag.Estados = GetEstados(cadastro.IdEstado);
                return View(cadastro);
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.TipoPessoa = GetTipoPessoa(cadastro.TipoPessoa);
                ViewBag.TipoEndereco = GetTipoEndereco(cadastro.TipoEndereco);
                ViewBag.Estados = GetEstados(cadastro.IdEstado);
                return View(cadastro);
            }
        }

        // GET: /Admin/Empresa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cadastro = serviceCadastro.Find((int)id);

            if (cadastro == null)
            {
                return HttpNotFound();
            }

            return View(cadastro);
        }

        // POST: /Admin/Empresa/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                serviceCadastro.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var cadastro = serviceCadastro.Find(id);
                if (cadastro == null)
                {
                    return HttpNotFound();
                }
                return View(cadastro);
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

using Salao.Domain.Abstract.Admin;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using Salao.Domain.Service.Endereco;
using Salao.Web.Areas.Empresa.Common;
using Salao.Web.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Salao.Web.Areas.Empresa.Controllers
{
    [AreaAuthorize("Empresa",Roles="empresa_crud")]
    public class CadastroEmpresaController : Controller
    {
        ICadastroEmpresa _service;

        public CadastroEmpresaController(ICadastroEmpresa service)
        {
            _service = service;
        }

        // GET: Empresa/CadastroEmpresa
        public ActionResult Index()
        {
            var cadastro = _service.Find(Identification.IdEmpresa);

            if (cadastro == null)
            {
                return HttpNotFound();
            }

            ViewBag.TipoPessoa = GetTipoPessoa(cadastro.TipoPessoa);
            ViewBag.TipoEndereco = GetTipoEndereco(cadastro.TipoEndereco);
            ViewBag.Estados = GetEstados(cadastro.IdEstado);
            return View(cadastro);
        }

        // POST: Empresa/CadastroEmpresa
        [HttpPost]
        public ActionResult Index([Bind(Include = "Id,IdEndereco,Fantasia,RazaoSocial,TipoPessoa,Cnpj,Cpf,TipoEndereco,Cep,Logradouro,Numero,Bairro,Cidade,IdEstado,Contato,Email,DDD,Telefone,Observ,Cortesia,Desconto,DescontoCarencia,CadastradoPor")] CadastroEmpresa cadastro)
        {
            try
            {
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
                    _service.Gravar(cadastro);
                    return RedirectToAction("Index", "Home", new { mensagem = "Cadastro atualizado" });
                }

                ViewBag.TipoPessoa = GetTipoPessoa(cadastro.TipoPessoa);
                ViewBag.TipoEndereco = GetTipoEndereco(cadastro.TipoEndereco);
                ViewBag.Estados = GetEstados(cadastro.IdEstado);
                return View(cadastro);
            }
            catch (System.Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.TipoPessoa = GetTipoPessoa(cadastro.TipoPessoa);
                ViewBag.TipoEndereco = GetTipoEndereco(cadastro.TipoEndereco);
                ViewBag.Estados = GetEstados(cadastro.IdEstado);
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
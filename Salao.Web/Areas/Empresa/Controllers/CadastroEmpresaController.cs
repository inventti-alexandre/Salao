using Salao.Domain.Abstract.Admin;
using Salao.Domain.Models.Cliente;
using Salao.Web.Areas.Empresa.Common;
using Salao.Web.Common;
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

                return View(cadastro);
            }
            catch (System.Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(cadastro);
            }
        }
    }
}
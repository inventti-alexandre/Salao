using Salao.Domain.Abstract;
using Salao.Domain.Service.Cliente;
using Salao.Web.Areas.Empresa.Common;
using Salao.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Salao.Web.Areas.Empresa.Controllers
{
    [AreaAuthorize("Empresa",Roles="empresa_master,empresa_mananger")]
    public class CadastroEmpresaController : Controller
    {
        IBaseService<Salao.Domain.Models.Cliente.Empresa> service;

        public CadastroEmpresaController()
        {
            this.service = new EmpresaService();
        }

        // GET: Empresa/CadastroEmpresa
        public ActionResult Index()
        {
            var empresa = service.Find(Identification.IdEmpresa);

            if (empresa == null)
            {
                return HttpNotFound();
            }

            return View(empresa);
        }
    }
}
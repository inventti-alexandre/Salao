using Salao.Domain.Abstract;
using Salao.Domain.Service.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Salao.Web.Areas.Cliente.Controllers
{
    public class CadastroEmpresaController : Controller
    {
        IBaseService<Salao.Domain.Models.Cliente.Empresa> service;

        public CadastroEmpresaController()
        {
            this.service = new EmpresaService();
        }

        // GET: Cliente/CadastroEmpresa
        public ActionResult Index()
        {
            return View();
        }
    }
}
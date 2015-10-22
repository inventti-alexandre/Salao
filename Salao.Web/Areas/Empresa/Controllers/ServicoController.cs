using Salao.Domain.Abstract;
using Salao.Domain.Models.Admin;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Admin;
using Salao.Domain.Service.Cliente;
using Salao.Web.Areas.Empresa.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Salao.Web.Areas.Empresa.Controllers
{
    public class ServicoController : Controller
    {
        IBaseService<Servico> service;
        IBaseService<Salao.Domain.Models.Cliente.Salao> serviceSalao;
        IBaseService<Area> serviceArea;
        IBaseService<SubArea> serviceSubArea;

        public ServicoController()
        {
            service = new ServicoService();
            serviceSalao = new SalaoService();
            serviceArea = new AreaService();
            serviceSubArea = new SubAreaService();
        }

        // GET: Empresa/Servico
        public ActionResult Index(int idSalao = 0, int idArea = 0, int idSubArea = 0)
        {
            if (idSalao == 0)
            {
                var salao = serviceSalao.Listar()
                    .Where(x => x.IdEmpresa == Identification.IdEmpresa)
                    .OrderBy(x => x.Fantasia)
                    .FirstOrDefault();

                if (salao == null)
                {
                    return RedirectToAction("Filial");
                }
                else
                {
                    idSalao = salao.Id;
                }
            }

            if (idArea == 0)
            {
                idArea = serviceArea.Listar().OrderBy(x => x.Descricao).FirstOrDefault().Id;
            }

            if (idSubArea == 0)
            {
                idSubArea = serviceSubArea.Listar().Where(x => x.IdArea == idArea).OrderBy(x => x.Descricao).First().Id;
            }

            ViewBag.IdSalao = idSalao;
            ViewBag.Saloes = GetSelectSaloes(idSalao);
            ViewBag.Areas = GetSelectAreas(idArea);
            ViewBag.SubAreas = GetSelectSubAreas(idArea, idSubArea);

            return View();
        }

        public PartialViewResult ServicosPrestados(int idSalao, int idArea = 0, int idSubArea = 0)
        {
            var salao = serviceSalao.Find(idSalao);

            var servicos = service.Listar()
                .Where(x => x.IdSalao == idSalao
                 && (idSubArea == 0 || x.IdSubArea == idSubArea))
                .OrderBy(x => x.Descricao)
                .ToList();

            ViewBag.IdSalao = idSalao;
            ViewBag.IdArea = idArea;
            ViewBag.IdSubArea = idSubArea;
            ViewBag.Fantasia = salao.Fantasia;
            ViewBag.Endereco = string.Format("{0}, {1}", salao.Endereco.Logradouro, salao.Endereco.Numero);

            return PartialView("ServicosPrestados", servicos);
        }

        // GET: Empresa/Servico/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Empresa/Servico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empresa/Servico/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Empresa/Servico/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Empresa/Servico/Edit/5
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

        // GET: Empresa/Servico/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Empresa/Servico/Delete/5
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

        public JsonResult GetSubAreas(int idArea)
        {
            if (HttpContext.Request.IsAjaxRequest())
            {
                int idSubArea = serviceSubArea.Listar().Where(x => x.IdArea == idArea).OrderBy(x => x.Descricao).First().Id;
                return Json(GetSelectSubAreas(idArea, idSubArea), JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        #region [ Privates ]

        private SelectList GetSelectSaloes(int idSalao)
        {
            return new SelectList(
               serviceSalao.Listar().Where(x => x.IdEmpresa == Identification.IdEmpresa).OrderBy(x => x.Fantasia).ToList(),
               "Id",
               "Fantasia",
               idSalao);
        }

        private SelectList GetSelectAreas(int idArea)
        {
            return new SelectList(
                serviceArea.Listar().Where(x => x.Ativo == true).OrderBy(x => x.Descricao).ToList(),
                "Id",
                "Descricao",
                idArea);
        }

        private SelectList GetSelectSubAreas(int idArea, int idSubArea)
        {
            return new SelectList(
                serviceSubArea.Listar().Where(x => x.Ativo == true && (idArea == 0 || x.IdArea == idArea)).ToList(),
                "Id",
                "Descricao",
                idSubArea);
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using Salao.Domain.Service.Endereco;
using System.Net;

namespace Salao.Web.Areas.Cliente.Controllers
{
    public class SalaoController : Controller
    {
        IBaseService<Salao.Domain.Models.Cliente.Salao> service;
        IBaseService<Empresa> serviceEmpresa;

        public SalaoController()
        {
            service = new Salao.Domain.Service.Cliente.SalaoService();
            serviceEmpresa = new EmpresaService();
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
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Cliente/Salao/Create
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

        //
        // GET: /Cliente/Salao/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Cliente/Salao/Edit/5
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
    }
}

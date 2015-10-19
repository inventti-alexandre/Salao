using Salao.Domain.Abstract;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using Salao.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Empresa.Controllers
{
    public class GrupoController : Controller
    {
        private IBaseService<CliGrupo> service;

        public GrupoController()
        {
            service = new CliGrupoService();
        }

        // GET: Empresa/Grupo
        public ActionResult Index()
        {

            return View();
        }

        // GET: Empresa/Grupo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Empresa/Grupo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empresa/Grupo/Create
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

        // GET: Empresa/Grupo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Empresa/Grupo/Edit/5
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

        // GET: Empresa/Grupo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Empresa/Grupo/Delete/5
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

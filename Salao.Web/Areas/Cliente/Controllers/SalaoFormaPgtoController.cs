using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Salao.Web.Areas.Cliente.Controllers
{
    public class SalaoFormaPgtoController : Controller
    {
        //
        // GET: /Cliente/SalaoFormaPgto/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Cliente/SalaoFormaPgto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Cliente/SalaoFormaPgto/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Cliente/SalaoFormaPgto/Create
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
        // GET: /Cliente/SalaoFormaPgto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Cliente/SalaoFormaPgto/Edit/5
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
        // GET: /Cliente/SalaoFormaPgto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Cliente/SalaoFormaPgto/Delete/5
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

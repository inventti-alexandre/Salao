using Salao.Domain.Abstract;
using Salao.Domain.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Salao.Web.Controllers
{
    public class ParceriaController : Controller
    {
        private IBaseService<PreContato> service;

        //
        // GET: /Parceria/
        public ActionResult Index()
        {
            return View(new PreContato());
        }
	}
}
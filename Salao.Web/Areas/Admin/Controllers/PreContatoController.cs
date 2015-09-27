using Salao.Domain.Abstract;
using Salao.Domain.Models.Admin;
using Salao.Domain.Service.Admin;
using Salao.Domain.Service.Endereco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class PreContatoController : Controller
    {
        IBaseService<PreContato> service;

        public PreContatoController()
        {
            service = new PreContatoService();
        }

        // GET: Admin/PreContato
        public ActionResult Index(string email = "")
        {
            IEnumerable<PreContato> contatos;

            if (!string.IsNullOrEmpty(email))
            {
                contatos = service.Listar().Where(x => x.Email == email).ToList();
            }
            else
            {
                contatos = new List<PreContato>();
            }

            return View(contatos);
        }

        // GET: Admin/PreContato/Details/5
        public ActionResult Details(int id)
        {
            var contato = service.Find(id);

            if (contato == null)
            {
                return HttpNotFound();
            }

            return View(contato);
        }

        // GET: Admin/PreContato/Create
        public ActionResult Create()
        {
            ViewBag.Estados = GetEstados();
            return View(new PreContato());
        }

        // POST: Admin/PreContato/Create
        [HttpPost]
        public ActionResult Create(PreContato contato)
        {
            try
            {
                contato.ContatoEm = DateTime.Now;
                contato.Observ = string.Empty;
                TryUpdateModel(contato);

                if (ModelState.IsValid)
                {
                    service.Gravar(contato);
                    return RedirectToAction("Index", new { email = contato.Email });                    
                }
                return View(contato);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(contato);
            }
        }

        // GET: Admin/PreContato/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var contato = service.Find((int)id);

            if (contato == null)
            {
                return HttpNotFound();
            }

            ViewBag.Estados = GetEstados(contato.IdEstado);
            return View(contato);
        }

        // POST: Admin/PreContato/Edit/5
        [HttpPost]
        public ActionResult Edit(PreContato contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Gravar(contato);
                    return RedirectToAction("Index", new { email = contato.Email });
                }
                ViewBag.Estados = GetEstados(contato.IdEstado);    
                return View(contato);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.Estados = GetEstados(contato.IdEstado);
                return View(contato);
            }
        }

        // GET: Admin/PreContato/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var contato = service.Find((int)id);

            if (contato == null)
            {
                return HttpNotFound();
            }

            return View(contato);
        }

        // POST: Admin/PreContato/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var contato = service.Find(id);
                if (contato == null)
                {
                    return HttpNotFound();
                }
                return View(contato);
            }
        }

        // GET: Admin/PreContato/Aprovados
        public ActionResult Aprovados()
        {
            return View();
        }

        // GET: Admin/PreContato/Rejeitados()
        public ActionResult Rejeitados()
        {
            return View();
        }
        
        private SelectList GetEstados(int id = 0)
        {
            return new SelectList(new EstadoService().Listar().Where(x => x.Ativo == true).OrderBy(x => x.UF),
                "Id", "UF", id);
        }
    }
}

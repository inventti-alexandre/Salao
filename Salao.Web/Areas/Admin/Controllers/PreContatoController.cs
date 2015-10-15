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
    [Authorize(Roles = "admin")]
    public class PreContatoController : Controller
    {
        IBaseService<PreContato> service;

        public PreContatoController()
        {
            service = new PreContatoService();
        }

        // GET: Admin/PreContato
        public ActionResult Index(string email = "", string returnUrl = "")
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                Response.Redirect(Server.UrlDecode(returnUrl));
            }

            IEnumerable<PreContato> contatos;

            if (!string.IsNullOrEmpty(email))
            {
                contatos = service.Listar().Where(x => x.Email == email).ToList();
            }
            else
            {
                contatos = service.Listar().Where(x => x.Atendido == false && x.ContatarNovamente == false).ToList();
            }

            return View(contatos);
        }

        // GET: Admin/PreContato/Details/5
        public ActionResult Details(int id, string returnUrl = "")
        {
            var contato = service.Find(id);

            if (contato == null)
            {
                return HttpNotFound();
            }

            ViewBag.returnUrl = returnUrl;
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
        public ActionResult Edit(int? id, string returnUrl = "")
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                Response.Redirect(Server.UrlDecode(returnUrl));
            }

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
            ViewBag.returnUrl = returnUrl;
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
        public ActionResult Aprovados(DateTime? inicial, DateTime? final)
        {
            if (inicial == null)
            {
                inicial = (DateTime.Today.DayOfWeek == DayOfWeek.Monday ? DateTime.Today.Date.AddDays(-2) : DateTime.Today.Date);
            }

            if (final == null)
            {
                final = DateTime.Today.Date;
            }
            final = ((DateTime)final).AddHours(23).AddMinutes(59);

            var contatos = service.Listar().Where(x => x.ContatoEm >= inicial && x.ContatoEm <= final && x.Assinou == true).ToList();

            ViewBag.Inicial = inicial;
            ViewBag.Final = final;
            return View(contatos);
        }

        // GET: Admin/PreContato/ContatarNovamente
        public ActionResult ContatarNovamente(DateTime? contatoEm)
        {
            if (contatoEm == null)
            {
                contatoEm = DateTime.Today.Date.Subtract(TimeSpan.FromDays(DateTime.Today.Date.Day - 1)).AddMonths(-1);
            }

            var contatos = service.Listar()
                .Where(x => x.ContatarNovamente == true && x.Assinou == false && x.ContatoEm >= contatoEm && x.Ativo == true)
                .ToList();

            ViewBag.ContatoEm = contatoEm;
            return View(contatos);
        }
        
        private SelectList GetEstados(int id = 0)
        {
            return new SelectList(new EstadoService().Listar().Where(x => x.Ativo == true).OrderBy(x => x.UF),
                "Id", "UF", id);
        }

        // GET: Admin/PreContato/Inativar
        public ActionResult Inativar(int? id)
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

        // POST: Admin/PreContato/Inativar
        [HttpPost]
        public ActionResult Inativar(int id)
        {
            var contato = service.Find(id);

            if (contato == null)
            {
                return HttpNotFound();
            }

            try
            {
                contato.Ativo = false;
                service.Gravar(contato);
                return RedirectToAction("ContatarNovamente");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(contato);
            }
        }

        // GET: Admin/PreContato/Contato
        public ActionResult Contato(int? id)
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

        // POST: Admin/PreContato/Contato
        [HttpPost]
        public ActionResult Contato(int id, string email, string telefone, bool contatarNovamente, string comentarios)
        {
            var contato = service.Find(id);

            if (contato == null)
            {
                return HttpNotFound();
            }

            try
            {
                contato.Email = email;
                contato.Telefone = telefone;
                contato.ContatarNovamente = contatarNovamente;
                contato.Observ = string.Format("-- {0} {1} {2} {3} {4} {5}", DateTime.Now.ToString(), Environment.NewLine, comentarios, Environment.NewLine, Environment.NewLine, contato.Observ);
                contato.ContatoEm = DateTime.Now;
                service.Gravar(contato);
                return RedirectToAction("ContatarNovamente");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(contato);
            }
        }

        // GET: Admin/PreContato/ContatosDia
        public ActionResult ContatosDia(DateTime? inicial, DateTime? final)
        {
            if (inicial == null)
            {
                inicial = (DateTime.Today.DayOfWeek == DayOfWeek.Monday ? DateTime.Today.Date.AddDays(-2) : DateTime.Today.Date);
            }

            if (final == null)
	        {
                final = DateTime.Today.Date;
	        }
            final = ((DateTime)final).AddHours(23).AddMinutes(59);

            var contatos = service.Listar().Where(x => x.ContatoEm >= inicial && x.ContatoEm <= final).ToList();

            ViewBag.Inicial = inicial;
            ViewBag.final = final;
            return View(contatos);
        }

        // GET: Admin/PreContato/Rejeitados
        public ActionResult Rejeitados(DateTime? inicial, DateTime? final)
        {
            if (inicial == null)
            {
                inicial = (DateTime.Today.DayOfWeek == DayOfWeek.Monday ? DateTime.Today.Date.AddDays(-2) : DateTime.Today.Date);
            }

            if (final == null)
            {
                final = DateTime.Today.Date;
            }
            final = ((DateTime)final).AddHours(23).AddMinutes(59);
            
            var contatos = service.Listar().Where(x => x.ContatoEm >= inicial && x.ContatoEm <= final && x.Ativo == false).ToList();

            ViewBag.Inicial = inicial;
            ViewBag.Final = final;
            return View(contatos);
        }

    }
}

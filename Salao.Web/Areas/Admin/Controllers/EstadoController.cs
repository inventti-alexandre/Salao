using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Salao.Domain.Abstract.Endereco;
using Salao.Domain.Models.Endereco;
using Salao.Domain.Service.Endereco;
using Salao.Domain.Service.Admin;
using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Admin;
using System.Net;

namespace Salao.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class EstadoController : Controller
    {
        private IBaseService<EnderecoEstado> service;

        public EstadoController()
        {
            service = new EstadoService();
        }

        // GET: Admin/Estado
        public ActionResult Index()
        {
            var estados = service.Listar()
                .OrderBy(x => x.Descricao)
                .ToList();

            return View(estados);
        }

        // GET: Admin/Estado/Details/5
        public ActionResult Details(int id)
        {
            var estado = service.Find(id);

            if (estado == null)
            {
                return HttpNotFound();
            }

            return View(estado);
        }

        // GET: Admin/Estado/Create
        public ActionResult Create()
        {
            return View(new EnderecoEstado { Ativo = true });
        }

        // POST: Admin/Estado/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="Descricao,UF")] EnderecoEstado estado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Gravar(estado);
                    return RedirectToAction("Index");                    
                }
                return View(estado);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(estado);
            }
        }

        // GET: Admin/Estado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var estado = service.Find((int)id);

            if (estado == null)
            {
                return HttpNotFound();
            }

            return View(estado);
        }

        // POST: Admin/Estado/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include="Id,Descricao,UF,Ativo")] EnderecoEstado estado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Gravar(estado);
                    return RedirectToAction("Index");                    
                }
                return View(estado);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(estado);
            }
        }

        // GET: Admin/Estado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var estado = service.Find((int)id);

            if (estado == null)
            {
                return HttpNotFound();
            }

            return View(estado);
        }

        // POST: Admin/Estado/Delete/5
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
                var estado = service.Find(id);
                if (estado == null)
                {
                    return HttpNotFound();
                }
                return View(estado);
            }
        }
    }
}

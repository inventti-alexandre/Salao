using Salao.Domain.Abstract;
using Salao.Domain.Models.Endereco;
using Salao.Domain.Service.Endereco;
using Salao.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [AreaAuthorizeAttribute("Admin", Roles="admin")]
    public class EstadoController : Controller
    {
        private IBaseService<EnderecoEstado> _service;

        public EstadoController(IBaseService<EnderecoEstado> service)
        {
            _service = service;
        }

        // GET: Admin/Estado
        public ActionResult Index()
        {
            var estados = _service.Listar()
                .OrderBy(x => x.Descricao)
                .ToList();

            return View(estados);
        }

        // GET: Admin/Estado/Details/5
        public ActionResult Details(int id)
        {
            var estado = _service.Find(id);

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
                    _service.Gravar(estado);
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

            var estado = _service.Find((int)id);

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
                    _service.Gravar(estado);
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

            var estado = _service.Find((int)id);

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
                _service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var estado = _service.Find(id);
                if (estado == null)
                {
                    return HttpNotFound();
                }
                return View(estado);
            }
        }
    }
}

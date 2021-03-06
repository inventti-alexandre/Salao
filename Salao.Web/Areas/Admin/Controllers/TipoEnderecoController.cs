﻿using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Admin;
using Salao.Domain.Models.Endereco;
using Salao.Domain.Service.Admin;
using Salao.Domain.Service.Endereco;
using Salao.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [AreaAuthorizeAttribute("Admin", Roles="admin")]
    public class TipoEnderecoController : Controller
    {
        IBaseService<EnderecoTipoEndereco> _service;
        private ILogin _login;

        public TipoEnderecoController(IBaseService<EnderecoTipoEndereco> service, ILogin login)
        {
            _service = service;
            _login = login;
        }

        //
        // GET: /Admin/TipoEndereco/
        public ActionResult Index()
        {
            var tipos = _service.Listar()
                .OrderBy(x => x.Descricao);

            return View(tipos);
        }

        //
        // GET: /Admin/TipoEndereco/Details/5
        public ActionResult Details(int id)
        {
            var tipo = _service.Find(id);

            if (tipo == null)
            {
                return HttpNotFound();
            }

            return View(tipo);
        }

        //
        // GET: /Admin/TipoEndereco/Create
        public ActionResult Create()
        {
            return View(new EnderecoTipoEndereco());
        }

        //
        // POST: /Admin/TipoEndereco/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="Descricao,Ativo")] EnderecoTipoEndereco tipo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.Gravar(tipo);
                    return RedirectToAction("Index");
                }

                return View(tipo);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View();
            }
        }

        //
        // GET: /Admin/TipoEndereco/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tipo = _service.Find((int)id);

            if (tipo == null)
            {
                return HttpNotFound();
            }

            return View(tipo);
        }

        //
        // POST: /Admin/TipoEndereco/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include="Id,Descricao,Ativo")] EnderecoTipoEndereco tipo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.Gravar(tipo);
                    return RedirectToAction("Index");                    
                }
                return View(tipo);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(tipo);
            }
        }

        //
        // GET: /Admin/TipoEndereco/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tipo = _service.Find((int)id);

            if (tipo == null)
            {
                return HttpNotFound();
            }

            return View(tipo);
        }

        //
        // POST: /Admin/TipoEndereco/Delete/5
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
                var tipo = _service.Find(id);
                if (tipo == null)
                {
                    return HttpNotFound();
                }
                return View(tipo);
            }
        }
    }
}

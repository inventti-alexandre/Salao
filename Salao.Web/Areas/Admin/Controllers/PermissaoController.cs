﻿using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Admin;
using Salao.Domain.Models.Admin;
using Salao.Domain.Service.Admin;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class PermissaoController : Controller
    {
        private IBaseService<Permissao> service;
        private ILogin login;

        public PermissaoController()
        {
            service = new PermissaoService();
            login = new UsuarioService();
        }

        //
        // GET: /Admin/Permissao/
        public ActionResult Index()
        {
            var permissoes = service.Listar()
                .OrderBy(x => x.Descricao);

            return View(permissoes);
        }

        //
        // GET: /Admin/Permissao/Details/5
        public ActionResult Details(int id)
        {
            var permissao = service.Find(id);

            if (permissao == null)
            {
                return HttpNotFound();
            }

            return View(permissao);
        }

        //
        // GET: /Admin/Permissao/Create
        public ActionResult Create()
        {
            return View(new Permissao());
        }

        //
        // POST: /Admin/Permissao/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="Descricao")] Permissao permissao)
        {
            try
            {
                permissao.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                permissao.AlteradoEm = DateTime.Now;
                TryUpdateModel(permissao);

                if (ModelState.IsValid)
                {
                    service.Gravar(permissao);
                    return RedirectToAction("Index");
                }

                return View(permissao);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(permissao);
            }
        }

        //
        // GET: /Admin/Permissao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var permissao = service.Find((int)id);

            if (permissao == null)
            {
                return HttpNotFound();
            }

            return View(permissao);
        }

        //
        // POST: /Admin/Permissao/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include="Id,Descricao,Ativo")] Permissao permissao)
        {
            try
            {
                permissao.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                TryUpdateModel(permissao);

                if (ModelState.IsValid)
                {
                    service.Gravar(permissao);
                    return RedirectToAction("Index");
                }

                return View(permissao);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(permissao);
            }
        }

        //
        // GET: /Admin/Permissao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var permissao = service.Find((int)id);

            if (permissao == null)
            {
                return HttpNotFound();
            }

            return View(permissao);
        }

        //
        // POST: /Admin/Permissao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch
            {
                var permissao = service.Find(id);
                if (permissao == null)
                {
                    return HttpNotFound();
                }
                return View(permissao);
            }
        }
    }
}
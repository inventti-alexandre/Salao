using Salao.Domain.Abstract;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Salao.Web.Common;

namespace Salao.Web.Areas.Admin.Controllers
{
    [AreaAuthorizeAttribute("Admin", Roles="admin")]
    public class ColaboradorController : Controller
    {
        IBaseService<Profissional> service;
        IBaseService<Salao.Domain.Models.Cliente.Salao> serviceSalao;

        public ColaboradorController()
        {
            service = new ProfissionalService();
            serviceSalao = new SalaoService();
        }

        // GET: Cliente/Colaborador
        public ActionResult Index(int idSalao)
        {
            var salao = serviceSalao.Find(idSalao);

            if (salao == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var profissionais = service.Listar()
                .Where(x => x.IdSalao == idSalao)
                .OrderBy(x => x.Nome);

            ViewBag.IdSalao = idSalao;
            ViewBag.IdEmpresa = salao.IdEmpresa;
            ViewBag.Fantasia = salao.Fantasia;
            return View(profissionais);
        }

        // GET: Cliente/Colaborador/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var colaborador = service.Find((int)id);

            if (colaborador == null)
            {
                return HttpNotFound();
            }

            ViewBag.Image = GetImage(colaborador.Id);
            return View(colaborador);
        }

        // GET: Cliente/Colaborador/Create
        public ActionResult Create(int idSalao)
        {
            var salao = serviceSalao.Find(idSalao);

            if (salao == null)
            {
                return HttpNotFound();
            }

            var profissional = new Profissional { IdSalao = idSalao };
            ViewBag.Fantasia = salao.Fantasia;

            return View(profissional);
        }

        // POST: Cliente/Colaborador/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="IdSalao,Nome,Telefone,Email")] Profissional profissional, HttpPostedFileBase image)
        {
            try
            {
                profissional.AlteradoEm = DateTime.Now;
                TryUpdateModel(profissional);

                if (ModelState.IsValid)
                {
                    var id = service.Gravar(profissional);
                    SetImage(image, id);
                    return RedirectToAction("Index", new { idSalao = profissional.IdSalao });
                }

                ViewBag.Fantasia = serviceSalao.Find(profissional.IdSalao).Fantasia;
                return View(profissional);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.Fantasia = serviceSalao.Find(profissional.IdSalao).Fantasia;
                return View(profissional);
            }
        }

        // GET: Cliente/Colaborador/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var profissional = service.Find((int)id);

            if (profissional == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Image = GetImage(profissional.Id);
            return View(profissional);
        }

        // POST: Cliente/Colaborador/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,IdSalao,Nome,Telefone,Email")] Profissional profissional, HttpPostedFileBase image)
        {
            try
            {
                profissional.AlteradoEm = DateTime.Now;
                TryUpdateModel(profissional);

                if (ModelState.IsValid)
                {
                    int id = service.Gravar(profissional);
                    SetImage(image, id);
                    return RedirectToAction("Index", new { idSalao = profissional.IdSalao });
                }
                return View(profissional);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(profissional);
            }
        }

        // GET: Cliente/Colaborador/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var colaborador = service.Find((int)id);

            if (colaborador == null)
            {
                return HttpNotFound();
            }

            ViewBag.Image = GetImage(colaborador.Id);
            return View(colaborador);
        }

        // POST: Cliente/Colaborador/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int idSalao)
        {
            try
            {
                var colaborador = service.Excluir(id);
                if (colaborador != null)
                {
                    DeleteImage(colaborador.Id);
                }
                return RedirectToAction("Index", new { idSalao = idSalao });
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var colaborador = service.Find(id);
                if (colaborador == null)
                {
                    return HttpNotFound();
                }
                return View(colaborador);
            }
        }


        #region [ privates ]

        private string GetImage(int id)
        {
            var systemFileName = id.ToString() + ".jpg";
            var path = Path.Combine(Server.MapPath("~/Content/Colaboradores/"), systemFileName);
            if (System.IO.File.Exists(path))
            {
                return string.Format("/Content/Colaboradores/{0}.{1}", id, "jpg");
            }

            return string.Empty;
        }

        private void DeleteImage(int id)
        {
            var systemFileName = id.ToString() + ".jpg";
            var path = Path.Combine(Server.MapPath("~/Content/Colaboradores/"), systemFileName);
            if (System.IO.File.Exists(path))
            {
                try
                {
                    System.IO.File.Delete(path);
                }
                catch (Exception)
                {
                    // none
                }
            }
        }

        private void SetImage(HttpPostedFileBase image, int id)
        {
            // grava imagem do funcionario
            if (image != null && image.ContentLength > 0)
            {
                var extensao = Path.GetExtension(image.FileName);
                if (extensao.ToLower().Contains("jpg"))
                {
                    var systemFileName = id.ToString() + extensao;
                    var path = Path.Combine(Server.MapPath("~/Content/Colaboradores/"), systemFileName);
                    image.SaveAs(path);
                }
            }
        }

        #endregion
    }
}

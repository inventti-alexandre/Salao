using Salao.Domain.Abstract;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using Salao.Web.Areas.Empresa.Common;
using Salao.Web.Common;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Salao.Web.Areas.Empresa.Controllers
{
    [AreaAuthorize("Empresa", Roles="colaborador_crud")]
    public class ColaboradorController : Controller
    {
        IBaseService<Profissional> _service;
        IBaseService<Salao.Domain.Models.Cliente.Salao> _serviceSalao;

        public ColaboradorController(IBaseService<Profissional> service, IBaseService<Salao.Domain.Models.Cliente.Salao> serviceSalao)
        {
            _service = service;
            _serviceSalao = serviceSalao;
        }

        // GET: Empresa/Colaborador
        public ActionResult Index(int idSalao = 0)
        {
            if (idSalao == 0)
            {
                var primeiroSalao = _serviceSalao.Listar().FirstOrDefault();
                if (primeiroSalao != null)
                {
                    idSalao = primeiroSalao.Id;
                }
                else
                {
                    return RedirectToAction("Index", "Filial");
                }
            }

            var salao = _serviceSalao.Find(idSalao);

            if (salao == null || salao.IdEmpresa != Identification.IdEmpresa)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.IdSalao = idSalao;
            return View();
        }

        public PartialViewResult Colaboradores(int idSalao)
        {
            var salao = _serviceSalao.Find(idSalao);
            var profissionais = _service.Listar().Where(x => x.IdSalao == idSalao).OrderBy(x => x.Nome);

            ViewBag.SalaoFantasia = salao.Fantasia;
            ViewBag.SalaoEndereco = string.Format("{0}, {1}", salao.Endereco.Logradouro, salao.Endereco.Numero);
            return PartialView(profissionais);
        }

        // GET: Empresa/Colaborador/Details/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var profissional = _service.Find((int)id);

            if (profissional == null || profissional.Empresa.Id != Identification.IdEmpresa)
            {
                return HttpNotFound();
            }

            ViewBag.IdSalao = profissional.IdSalao;
            ViewBag.SalaoFantasia = profissional.Salao.Fantasia;
            ViewBag.SalaoEndereco = string.Format("{0}, {1}", profissional.Salao.Endereco.Logradouro, profissional.Salao.Endereco.Numero);
            ViewBag.Image = GetImage(profissional.Id);

            return View(profissional);
        }

        // GET: Empresa/Colaborador/Create
        public ActionResult Incluir(int idSalao)
        {
            var salao = _serviceSalao.Find(idSalao);

            if (salao == null || salao.IdEmpresa != Identification.IdEmpresa)
            {
                return HttpNotFound();
            }

            var profissional = new Profissional { IdSalao = idSalao };
            ViewBag.SalaoFantasia = salao.Fantasia;
            ViewBag.SalaoEndereco = string.Format("{0}, {1}", profissional.Salao.Endereco.Logradouro, profissional.Salao.Endereco.Numero);
            ViewBag.IdSalao = salao.Id;

            return View(profissional);
        }

        // POST: Empresa/Colaborador/Create
        [HttpPost]
        public ActionResult Incluir([Bind(Include = "IdSalao,Nome,Telefone,Email")] Profissional profissional, HttpPostedFileBase image)
        {
            try
            {
                profissional.AlteradoEm = DateTime.Now;
                TryUpdateModel(profissional);

                if (ModelState.IsValid)
                {
                    var id = _service.Gravar(profissional);
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

        // GET: Empresa/Colaborador/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var profissional = _service.Find((int)id);

            if (profissional == null || profissional.Empresa.Id != Identification.IdEmpresa)
            {
                return HttpNotFound();
            }

            ViewBag.SalaoFantasia = profissional.Salao.Fantasia;
            ViewBag.SalaoEndereco = string.Format("{0}, {1}", profissional.Salao.Endereco.Logradouro, profissional.Salao.Endereco.Numero);
            ViewBag.Image = GetImage(profissional.Id);

            return View(profissional);
        }

        // POST: Empresa/Colaborador/Edit/5
        [HttpPost]
        public ActionResult Editar([Bind(Include="Id,IdSalao,Nome,Telefone,Email")] Profissional profissional, HttpPostedFileBase image)
        {
            try
            {
                profissional.AlteradoEm = DateTime.Now;
                TryUpdateModel(profissional);

                if (ModelState.IsValid)
                {
                    int id = _service.Gravar(profissional);
                    SetImage(image, id);
                    return RedirectToAction("Index", new { idSalao = profissional.IdSalao });
                }

                ViewBag.SalaoFantasia = profissional.Salao.Fantasia;
                ViewBag.SalaoEndereco = string.Format("{0}, {1}", profissional.Salao.Endereco.Logradouro, profissional.Salao.Endereco.Numero);
                ViewBag.Image = GetImage(profissional.Id);
                return View(profissional);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.SalaoFantasia = profissional.Salao.Fantasia;
                ViewBag.SalaoEndereco = string.Format("{0}, {1}", profissional.Salao.Endereco.Logradouro, profissional.Salao.Endereco.Numero);
                ViewBag.Image = GetImage(profissional.Id);
                return View(profissional);
            }
        }

        // GET: Empresa/Colaborador/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var profissional = _service.Find((int)id);

            if (profissional == null || profissional.Empresa.Id != Identification.IdEmpresa)
            {
                return HttpNotFound();
            }

            ViewBag.SalaoFantasia = profissional.Salao.Fantasia;
            ViewBag.SalaoEndereco = string.Format("{0}, {1}", profissional.Salao.Endereco.Logradouro, profissional.Salao.Endereco.Numero);
            ViewBag.Image = GetImage(profissional.Id);

            return View(profissional);
        }

        // POST: Empresa/Colaborador/Delete/5
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            try
            {
                var profissional = _service.Excluir(id);
                return RedirectToAction("Index", new { idSalao = profissional.IdSalao });
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var profissional = _service.Find(id);
                if (profissional == null)
                {
                    return HttpNotFound();
                }
                return View(profissional);
            }
        }

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
    }
}

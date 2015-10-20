using Salao.Domain.Abstract;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using Salao.Web.Areas.Empresa.Common;
using Salao.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Empresa.Controllers
{
    [AreaAuthorize("Empresa", Roles="colaborador_crud")]
    public class ColaboradorController : Controller
    {
        IBaseService<Profissional> service;
        IBaseService<Salao.Domain.Models.Cliente.Salao> serviceSalao;

        public ColaboradorController()
        {
            service = new ProfissionalService();
            serviceSalao = new SalaoService();
        }

        // GET: Empresa/Colaborador
        public ActionResult Index(int idSalao = 0)
        {
            if (idSalao == 0)
            {
                var primeiroSalao = serviceSalao.Listar().FirstOrDefault();
                if (primeiroSalao != null)
                {
                    idSalao = primeiroSalao.Id;
                }
                else
                {
                    return RedirectToAction("Index", "Filial");
                }
            }

            var salao = serviceSalao.Find(idSalao);

            if (salao == null || salao.IdEmpresa != Identification.IdEmpresa)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var profissionais = service.Listar()
                .Where(x => x.IdSalao == idSalao)
                .OrderBy(x => x.Nome)
                .ToList();

            ViewBag.IdSalao = idSalao;
            ViewBag.SalaoFantasia = salao.Fantasia;
            ViewBag.SalaoEndereco = salao.Endereco.Logradouro;
            var saloes = GetSaloes(idSalao);
            ViewBag.Saloes = saloes;
            return View(profissionais);
        }

        // GET: Empresa/Colaborador/Details/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var profissional = service.Find((int)id);

            if (profissional == null || profissional.Empresa.Id != Identification.IdEmpresa)
            {
                return HttpNotFound();
            }

            ViewBag.IdSalao = profissional.IdSalao;
            ViewBag.SalaoFantasia = profissional.Salao.Fantasia;
            ViewBag.SalaoEndereco = profissional.Salao.Endereco.Logradouro;
            return View(profissional);
        }

        // GET: Empresa/Colaborador/Create
        public ActionResult Incluir(int idSalao)
        {
            var salao = serviceSalao.Find(idSalao);

            if (salao == null || salao.IdEmpresa != Identification.IdEmpresa)
            {
                return HttpNotFound();
            }

            var profissional = new Profissional { IdSalao = idSalao };
            ViewBag.SalaoFantasia = salao.Fantasia;
            ViewBag.SalaoEndereco = salao.Endereco.Logradouro;            
            
            return View(profissional);
        }

        // POST: Empresa/Colaborador/Create
        [HttpPost]
        public ActionResult Incluir([Bind(Include = "IdSalao,Nome,Telefone,Email")] Profissional profissional)
        {
            try
            {
                profissional.AlteradoEm = DateTime.Now;
                TryUpdateModel(profissional);

                if (ModelState.IsValid)
                {
                    service.Gravar(profissional);
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

            var profissional = service.Find((int)id);

            if (profissional == null || profissional.Empresa.Id != Identification.IdEmpresa)
            {
                return HttpNotFound();
            }

            ViewBag.SalaoFantasia = profissional.Salao.Fantasia;
            ViewBag.SalaoEndereco = profissional.Salao.Endereco.Logradouro;
            
            return View(profissional);
        }

        // POST: Empresa/Colaborador/Edit/5
        [HttpPost]
        public ActionResult Editar([Bind(Include="Id,IdSalao,Nome,Telefone,Email")] Profissional profissional)
        {
            try
            {
                profissional.AlteradoEm = DateTime.Now;
                TryUpdateModel(profissional);

                if (ModelState.IsValid)
                {
                    service.Gravar(profissional);
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

        // GET: Empresa/Colaborador/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var profissional = service.Find((int)id);

            if (profissional == null || profissional.Empresa.Id != Identification.IdEmpresa)
            {
                return HttpNotFound();
            }

            ViewBag.SalaoFantasia = profissional.Salao.Fantasia;
            ViewBag.SalaoEndereco = profissional.Salao.Endereco.Logradouro;

            return View(profissional);
        }

        // POST: Empresa/Colaborador/Delete/5
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            try
            {
                var profissional = service.Excluir(id);
                return RedirectToAction("Index", new { idSalao = profissional.IdSalao });
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var profissional = service.Find(id);
                if (id == null)
                {
                    return HttpNotFound();
                }
                return View(profissional);
            }
        }
        
        private SelectList GetSaloes(int idSalao = 0)
        {
            var saloes = serviceSalao.Listar()
                .Where(x => x.IdEmpresa == Identification.IdEmpresa
                && x.Ativo == true)
                .OrderBy(x => x.Fantasia)
                .ToList();

            return new SelectList(saloes, "Id", "Fantasia", idSalao);
        }
    }
}

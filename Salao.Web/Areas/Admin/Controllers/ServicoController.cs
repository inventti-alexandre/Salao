using Salao.Domain.Abstract;
using Salao.Domain.Models.Admin;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Admin;
using Salao.Domain.Service.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ServicoController : Controller
    {
        IBaseService<Servico> service;
        IBaseService<Salao.Domain.Models.Cliente.Salao> serviceSalao;
        IBaseService<Area> serviceArea;
        IBaseService<SubArea> serviceSubArea;

        public ServicoController()
        {
            service = new ServicoService();
            serviceSalao = new Salao.Domain.Service.Cliente.SalaoService();
            serviceArea = new AreaService();
            serviceSubArea = new SubAreaService();
        }

        //
        // GET: /Cliente/Servico/
        public ActionResult Index(int idSalao)
        {
            if (idSalao == null)
            {
                return HttpNotFound();
            }

            var salao = serviceSalao.Find(idSalao);

            if (salao == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var servicos = service.Listar()
                .Where(x => x.IdSalao == idSalao)
                .OrderBy(x => x.Descricao);

            ViewBag.IdSalao = idSalao;
            ViewBag.Fantasia = salao.Fantasia;
            ViewBag.IdEmpresa = salao.IdEmpresa;

            return View(servicos);
        }

        //
        // GET: /Cliente/Servico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var servico = service.Find((int)id);

            if (service == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(servico);
        }

        //
        // GET: /Cliente/Servico/Create
        public ActionResult Create(int idSalao)
        {
            var servico = new Servico { IdSalao = idSalao };

            var areas = GetAreas();
            var area = areas.First().Text;
            int idArea = serviceArea.Listar().FirstOrDefault(x => x.Descricao == area).Id;
            
            ViewBag.Areas = GetAreas();
            ViewBag.SubAreas = GetSubAreas(idArea);

            return View(servico);
        }

        //
        // POST: /Cliente/Servico/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="IdSalao,Descricao,Detalhe,Tempo,PrecoSemDesconto,Preco")] Servico servico, int Areas, int SubAreas)
        {
            try
            {
                servico.AlteradoEm = DateTime.Now;
                servico.IdSubArea = SubAreas;
                TryUpdateModel(servico);

                if (ModelState.IsValid)
                {
                    service.Gravar(servico);
                    return RedirectToAction("Index", new { idSalao = servico.IdSalao });
                }

                ViewBag.Areas = GetAreas(Areas);
                ViewBag.SubAreas = GetSubAreas(Areas, SubAreas);
                return View(servico);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.Areas = GetAreas(Areas);
                ViewBag.SubAreas = GetSubAreas(Areas, SubAreas);
                return View(servico);
            }
        }

        //
        // GET: /Cliente/Servico/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();    
            }

            var servico = service.Find((int)id);

            if (servico == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Areas = GetAreas(servico.Area.Id);
            ViewBag.SubAreas = GetSubAreas(servico.Area.Id, servico.IdSubArea);

            return View(servico);
        }

        //
        // POST: /Cliente/Servico/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,IdSalao,Descricao,Detalhe,Tempo,PrecoSemDesconto,Preco")] Servico servico, int Areas, int SubAreas)
        {
            try
            {
                servico.AlteradoEm = DateTime.Now;
                servico.IdSubArea = SubAreas;
                TryUpdateModel(servico);

                if (ModelState.IsValid)
                {
                    service.Gravar(servico);
                    return RedirectToAction("Index", new { idSalao = servico.IdSalao });
                }

                ViewBag.Areas = GetAreas(servico.Area.Id);
                ViewBag.SubAreas = GetSubAreas(servico.Area.Id, servico.IdSubArea);

                return View(servico);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.Areas = GetAreas(servico.Area.Id);
                ViewBag.SubAreas = GetSubAreas(servico.Area.Id, servico.IdSubArea);
                return View(servico);
            }
        }

        //
        // GET: /Cliente/Servico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var servico = service.Find((int)id);

            if (service == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(servico);
        }

        //
        // POST: /Cliente/Servico/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int idSalao)
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index", new { idSalao = idSalao });
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var servico = service.Find(id);
                if (servico == null)
                {
                    return HttpNotFound();   
                }
                return View(servico);
            }
        }

        #region [ privates ]

        private List<SelectListItem> GetAreas(int id = 0)
        {
            var lista = new List<SelectListItem>();

            var areas = serviceArea.Listar()
                .OrderBy(x => x.Descricao);

            foreach (var item in areas)
            {
                lista.Add(new SelectListItem { Text = item.Descricao, Value = item.Id.ToString(), Selected = (item.Id == id) });
            }

            return lista;
        }

        private List<SelectListItem> GetSubAreas(int idArea = 0, int id = 0)
        {
            var lista = new List<SelectListItem>();

            var subs = serviceSubArea.Listar()
                .Where(x => idArea == 0 || x.IdArea == idArea)
                .OrderBy(x => x.Descricao);

            foreach (var item in subs)
            {
                lista.Add(new SelectListItem { Text = item.Descricao, Value = item.Id.ToString(), Selected = (item.Id == id) });
            }

            return lista;
        }

        #endregion
    }
}

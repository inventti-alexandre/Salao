using Salao.Domain.Abstract;
using Salao.Domain.Models.Admin;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Admin;
using Salao.Domain.Service.Cliente;
using Salao.Web.Areas.Empresa.Common;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Salao.Web.Areas.Empresa.Controllers
{
    public class ServicoController : Controller
    {
        IBaseService<Servico> _service;
        IBaseService<Salao.Domain.Models.Cliente.Salao> _serviceSalao;
        IBaseService<Area> _serviceArea;
        IBaseService<SubArea> _serviceSubArea;

        public ServicoController(IBaseService<Servico> service, IBaseService<Salao.Domain.Models.Cliente.Salao> serviceSalao, IBaseService<Area> serviceArea, IBaseService<SubArea> serviceSubArea)
        {
            _service = service;
            _serviceSalao = serviceSalao;
            _serviceArea = serviceArea;
            _serviceSubArea = serviceSubArea;
        }

        // GET: Empresa/Servico
        public ActionResult Index(int idSalao = 0, int idArea = 0, int idSubArea = 0)
        {
            if (idSalao == 0)
            {
                var salao = _serviceSalao.Listar()
                    .Where(x => x.IdEmpresa == Identification.IdEmpresa)
                    .OrderBy(x => x.Fantasia)
                    .FirstOrDefault();

                if (salao == null)
                {
                    return RedirectToAction("Filial");
                }
                else
                {
                    idSalao = salao.Id;
                }
            }

            if (idArea == 0)
            {
                idArea = _serviceArea.Listar().OrderBy(x => x.Descricao).FirstOrDefault().Id;
            }

            if (idSubArea == 0)
            {
                idSubArea = _serviceSubArea.Listar().Where(x => x.IdArea == idArea).OrderBy(x => x.Descricao).First().Id;
            }

            var saloes = GetSelectSaloes(idSalao);
            ViewBag.IdSalao = idSalao;
            ViewBag.ListaSaloes = saloes;
            ViewBag.ListaAreas = GetSelectAreas(idArea);
            ViewBag.ListaSubAreas = GetSelectSubAreas(idArea, idSubArea);

            return View();
        }

        public PartialViewResult ServicosPrestados(int idSalao, int idArea = 0, int idSubArea = 0)
        {
            var salao = _serviceSalao.Find(idSalao);

            var servicos = _service.Listar()
                .Where(x => x.IdSalao == idSalao
                 && (idSubArea == 0 || x.IdSubArea == idSubArea))
                .OrderBy(x => x.Descricao)
                .ToList();

            ViewBag.IdSalao = idSalao;
            ViewBag.IdArea = idArea;
            ViewBag.IdSubArea = idSubArea;
            ViewBag.Fantasia = salao.Fantasia;
            ViewBag.Endereco = string.Format("{0}, {1}", salao.Endereco.Logradouro, salao.Endereco.Numero);

            return PartialView("ServicosPrestados", servicos);
        }

        // GET: Empresa/Servico/Details/5
        public ActionResult Detalhes(int id)
        {
            var servico = _service.Find(id);

            // lista de saloes desta empresa
            if (!_serviceSalao.Listar().Where(x => x.IdEmpresa == Identification.IdEmpresa)
                .Select(x => x.Id).Contains(servico.IdSalao))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            return View(servico);
        }

        // GET: Empresa/Servico/Create
        public ActionResult Incluir(int idSalao, int idArea, int idSubArea)
        {
            var servico = new Servico { IdSalao = idSalao, IdSubArea = idSubArea };

            ViewBag.ListaAreas = GetSelectAreas(idArea);
            ViewBag.ListaSubAreas = GetSelectSubAreas(idArea, idSubArea);

            return View(servico);
        }

        // POST: Empresa/Servico/Create
        [HttpPost]
        public ActionResult Incluir([Bind(Include = "IdArea,IdSubArea,Descricao,Detalhe,Tempo,PrecoSemDesconto,Preco")] Servico servico)
        {
            try
            {
                servico.AlteradoEm = DateTime.Now;
                TryUpdateModel(servico);

                if (ModelState.IsValid)
                {
                    _service.Gravar(servico);
                    return RedirectToAction("Index", new { idSalao = servico.IdSalao, idArea = servico.Area.Id, idSubArea = servico.IdSubArea });
                }

                ViewBag.ListaAreas = GetSelectAreas(servico.Area.Id);
                ViewBag.ListaSubAreas = GetSelectSubAreas(servico.Area.Id, servico.IdSubArea);
                return View(servico);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.ListaAreas = GetSelectAreas(servico.Area.Id);
                ViewBag.ListaSubAreas = GetSelectSubAreas(servico.Area.Id, servico.IdSubArea);
                return View(servico);
            }
        }

        // GET: Empresa/Servico/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var servico = _service.Find((int)id);

            if (servico == null)
            {
                return HttpNotFound();
            }

            // lista de saloes desta empresa
            if (!_serviceSalao.Listar().Where(x => x.IdEmpresa == Identification.IdEmpresa)
                .Select(x => x.Id).Contains(servico.IdSalao))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.ListaAreas = GetSelectAreas(servico.SubArea.IdArea);
            ViewBag.ListaSubAreas = GetSelectSubAreas(servico.SubArea.IdArea, servico.IdSubArea);

            return View(servico);
        }

        // POST: Empresa/Servico/Edit/5
        [HttpPost]
        public ActionResult Editar([Bind(Include="Id,IdArea,IdSubArea,Descricao,Detalhe,Tempo,PrecoSemDesconto,Preco,Ativo")] Servico servico)
        {
            try
            {
                servico.AlteradoEm = DateTime.Now;
                TryUpdateModel(servico);

                if (ModelState.IsValid)
                {
                    _service.Gravar(servico);
                    return RedirectToAction("Index", new { idSalao = servico.IdSalao, idArea = servico.Area.Id, idSubArea = servico.IdSubArea });
                }

                ViewBag.ListaAreas = GetSelectAreas(servico.SubArea.IdArea);
                ViewBag.ListaSubAreas = GetSelectSubAreas(servico.SubArea.IdArea, servico.IdSubArea);
                return View(servico);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.ListaAreas = GetSelectAreas(servico.SubArea.IdArea);
                ViewBag.ListaSubAreas = GetSelectSubAreas(servico.SubArea.IdArea, servico.IdSubArea);
                return View(servico);
            }
        }

        // GET: Empresa/Servico/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var servico = _service.Find((int)id);

            if (servico == null)
            {
                return HttpNotFound();
            }
            
            // lista de saloes desta empresa
            if (!_serviceSalao.Listar().Where(x => x.IdEmpresa == Identification.IdEmpresa)
                .Select(x => x.Id).Contains(servico.IdSalao))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(servico);
        }

        // POST: Empresa/Servico/Delete/5
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            try
            {
                var servico = _service.Find(id);

                if (servico == null)
                {
                    return HttpNotFound();
                }

                // lista de saloes desta empresa
                if (!_serviceSalao.Listar().Where(x => x.IdEmpresa == Identification.IdEmpresa)
                    .Select(x => x.Id).Contains(servico.IdSalao))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                _service.Excluir(id);
                return RedirectToAction("Index", new { idSalao = servico.IdSalao, idArea = servico.Area.Id, idSubArea = servico.IdSubArea });
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var servico = _service.Find(id);
                if (servico == null)
                {
                    return HttpNotFound();
                }
                return View(servico);
            }
        }


        #region [ JsonResult ]

        public JsonResult GetSubAreas(int idArea)
        {
            if (HttpContext.Request.IsAjaxRequest())
            {
                int idSubArea = _serviceSubArea.Listar().Where(x => x.IdArea == idArea).OrderBy(x => x.Descricao).First().Id;
                return Json(GetSelectSubAreas(idArea, idSubArea), JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        #endregion


        #region [ Privates ]

        private SelectList GetSelectSaloes(int idSalao)
        {
            return new SelectList(
               _serviceSalao.Listar().Where(x => x.IdEmpresa == Identification.IdEmpresa).OrderBy(x => x.Fantasia).ToList(),
               "Id",
               "Fantasia",
               idSalao.ToString());
        }

        private SelectList GetSelectAreas(int idArea)
        {
            return new SelectList(
                _serviceArea.Listar().Where(x => x.Ativo == true).OrderBy(x => x.Descricao).ToList(),
                "Id",
                "Descricao",
                idArea.ToString());
        }

        private SelectList GetSelectSubAreas(int idArea, int idSubArea)
        {
            return new SelectList(
                _serviceSubArea.Listar().Where(x => x.Ativo == true && (idArea == 0 || x.IdArea == idArea)).ToList(),
                "Id",
                "Descricao",
                idSubArea.ToString());
        }

        #endregion
    }
}

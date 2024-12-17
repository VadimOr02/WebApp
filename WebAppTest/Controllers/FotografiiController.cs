using Repository_DBFirst;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models;
using BusinessLayer;

namespace WebAppTest.Controllers
{
    public class FotografiiController : Controller
    {
        private readonly IFotografiiService _fotografiiService;
        public FotografiiController(IFotografiiService fotografiiService)
        {
            _fotografiiService = fotografiiService;
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["UserId"] == null)
            {
                filterContext.Result = RedirectToAction("Login", "Account");
            }
            base.OnActionExecuting(filterContext);
        }
        public ActionResult Index()
        {
            var userId = Session["UserId"] as int?;
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account");
            var fotografii = _fotografiiService.GetFotografiiByUserId(userId.Value);
            return View(fotografii);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file)
        {
            int? userId = Session["UserId"] as int?;
            if (!userId.HasValue || file == null || file.ContentLength <= 0)
            {
                return RedirectToAction("Index");
            }

            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/Images"), fileName);
            file.SaveAs(path);

            var fotografie = new FotografiiViewModel
            {
                Utilizator_Id = userId.Value,
                Nume_Fisier = fileName,
                Cale = "~/Images/" + fileName,
                Data_Incarcare = DateTime.Now,
                Dimensiune = file.ContentLength,
                Format = file.ContentType
            };

            // Apelează metoda de adăugare a fotografiei din serviciu
            _fotografiiService.AddFotografie(fotografie);

            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var fotografie = _fotografiiService.GetFotografieById(id);
            if (fotografie == null)
            {
                return HttpNotFound();
            }

            var model = new FotografiiViewModel
            {
                Id = fotografie.id,
                Nume_Fisier = fotografie.nume_fisier,
                Cale = fotografie.cale
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConf(int id)
        {
            _fotografiiService.SoftDeleteFotografie(id);
            return RedirectToAction("Index");
        }
        public ActionResult EditareFotografie(int id)
        {
            var fotografie = _fotografiiService.GetFotografieById(id);
            if (fotografie == null)
            {
                return HttpNotFound();
            }
            var model = new FotografiiViewModel
            {
                Id = fotografie.id,
                Cale = fotografie.cale
            };

            return View(model);
        }
    }
}
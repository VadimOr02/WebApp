using Repository_DBFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTest.Models;

namespace WebAppTest.Controllers
{
    public class AbonamentController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["UserId"] == null)
            {
                filterContext.Result = RedirectToAction("Login", "Account");
            }
            base.OnActionExecuting(filterContext);
        }
        private readonly photoeditEntities _context;

        public AbonamentController(photoeditEntities context)
        {
            _context = context;
        }
        public AbonamentController() : this(new photoeditEntities())
        {
        }
        public ActionResult Index()
        {
            var abonaments = _context.Abonamente.ToList();

            return View(abonaments);
        }
        public ActionResult Details(int id)
        {
            var abonament = _context.Abonamente
               .FirstOrDefault(u => u.id == id);
            if (abonament == null)
            {
                return HttpNotFound();
            }

            return View(abonament);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AbonamentViewModel abonamentviewmodel)
        {
            if (ModelState.IsValid)
            {
                var abonament = new Abonamente
                {
                    nume = abonamentviewmodel.Nume,
                    pret = abonamentviewmodel.Pret,
                    durata = abonamentviewmodel.Durata,
                    beneficii = abonamentviewmodel.Beneficii
                };
                _context.Abonamente.Add(abonament);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(abonamentviewmodel);
        }
        public ActionResult Edit(int id)
        {
            var abonament = _context.Abonamente.Find(id);
            if (abonament == null)
            {
                return HttpNotFound();
            }

            var abonamentviewmodel = new AbonamentViewModel
            {
                Id = abonament.id,
                Nume = abonament.nume,
                Pret = abonament.pret,
                Durata = abonament.durata,
                Beneficii = abonament.beneficii
            };
            return View(abonamentviewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AbonamentViewModel abonamentViewModel)
        {
            if (ModelState.IsValid)
            {
                var abonament = _context.Abonamente.Find(abonamentViewModel.Id);
                if (abonament == null)
                {
                    return HttpNotFound();
                }

                abonament.nume = abonamentViewModel.Nume;
                abonament.pret = abonamentViewModel.Pret;
                abonament.durata = abonamentViewModel.Durata;
                abonament.beneficii = abonamentViewModel.Beneficii;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(abonamentViewModel);
        }
    }
}
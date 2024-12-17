using Repository_DBFirst;
using System;
using System.Linq;
using System.Web.Mvc;
using WebAppTest.Models;

namespace WebAppTest.Controllers
{
    public class UserController : Controller
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

        public UserController(photoeditEntities context)
        {
            _context = context;
        }
        public UserController() : this(new photoeditEntities())
        {
        }

        // GET: /User/Index - Returnează view-ul principal
        public ActionResult Index()
        {
            return View();
        }

        // GET: /User/GetAll - Returnează toți utilizatorii în format JSON
        [HttpGet]
        public JsonResult GetAll()
        {
            var users = _context.Utilizatori
                .Select(u => new
                {
                    Id = u.id,
                    Nume = u.nume,
                    Email = u.email,
                    AbonamentId = u.abonament_id,
                    Rol = u.rol,
                    DataInregistrare = u.data_inregistrare
                })
                .ToList()

            .Select(u => new
            {
                u.Id,
                u.Nume,
                u.Email,
                u.AbonamentId,
                u.Rol,
                DataInregistrare = u.DataInregistrare.ToString("yyyy-MM-ddTHH:mm:ssZ")
            })
        .ToList();

            return Json(users, JsonRequestBehavior.AllowGet);
        }

        // GET: /User/Get/{id} - Returnează un utilizator specific în format JSON
        [HttpGet]
        public JsonResult Get(int id)
        {
            var user = _context.Utilizatori
                .Where(u => u.id == id)
                .Select(u => new
                {
                    Id = u.id,
                    Nume = u.nume,
                    Email = u.email,
                    AbonamentId = u.abonament_id,
                    Rol = u.rol,
                    DataInregistrare = u.data_inregistrare
                })
                .FirstOrDefault();

            if (user == null)
            {
                return Json(new { error = "User not found" }, JsonRequestBehavior.AllowGet);
            }

            return Json(user, JsonRequestBehavior.AllowGet);
        }

        // POST: /User/Create - Creează un nou utilizator
        [HttpPost]
        public JsonResult Create(UserViewModel userviewmodel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { error = "Invalid data" });
            }

            var user = new Utilizatori
            {
                nume = userviewmodel.Nume,
                email = userviewmodel.Email,
                abonament_id = userviewmodel.Abonament_id,
                rol = userviewmodel.Rol,
                data_inregistrare = userviewmodel.Data_inregistrare ?? DateTime.Now,
                parola = userviewmodel.Parola
            };

            _context.Utilizatori.Add(user);
            _context.SaveChanges();

            return Json(new { success = true, userId = user.id });
        }

        // PUT: /User/Edit/{id} - Actualizează un utilizator existent
        [HttpPost]
        public JsonResult Edit(UserViewModel userviewmodel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { error = "Invalid data" });
            }

            var user = _context.Utilizatori.Find(userviewmodel.Id);
            if (user == null)
            {
                return Json(new { error = "User not found" });
            }

            user.nume = userviewmodel.Nume;
            user.email = userviewmodel.Email;
            user.abonament_id = userviewmodel.Abonament_id;
            user.rol = userviewmodel.Rol;
            user.parola = userviewmodel.Parola;

            _context.SaveChanges();

            return Json(new { success = true });
        }

        // DELETE: /User/Delete/{id} - Șterge un utilizator
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var user = _context.Utilizatori.Find(id);
            if (user == null)
            {
                return Json(new { error = "User not found" });
            }

            _context.Utilizatori.Remove(user);
            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
}

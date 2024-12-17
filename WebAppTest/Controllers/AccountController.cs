using Repository_DBFirst;
using System.Linq;
using System.Web.Mvc;
using WebAppTest.Models;

namespace WebAppTest.Controllers
{
    public class AccountController : Controller
    {
        private readonly photoeditEntities _context;

        public AccountController()
        {
            _context = new photoeditEntities();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _context.Utilizatori
                .FirstOrDefault(u => u.email == model.Email && u.parola == model.Parola);

            if (user == null)
            {
                ModelState.AddModelError("", "Email sau parolă incorectă.");
                return View(model);
            }

            // Salvăm utilizatorul autentificat în sesiune
            Session["UserId"] = user.id;
            Session["UserName"] = user.nume;

            return RedirectToAction("Index", "Fotografii");
        }

        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingUser = _context.Utilizatori.FirstOrDefault(u => u.email == model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Email-ul este deja utilizat.");
                return View(model);
            }

            var user = new Utilizatori
            {
                nume = model.Nume,
                email = model.Email,
                parola = model.Parola,
                rol = 1,
                data_inregistrare = System.DateTime.Now
            };

            _context.Utilizatori.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login", "Account");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }

    }
}
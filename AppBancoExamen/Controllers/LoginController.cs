using AppBancoExamen.FireBase;
using AppBancoExamen.Models;
using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppBancoExamen.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
     


        // GET: Login/Details/5
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                UserHelper userHelper = new UserHelper();

                UserCredential userCredential = await FireBaseAuthHelper.setFirebaseAuthClient().SignInWithEmailAndPasswordAsync(email, password);

                UserModel user = await userHelper.getUserInfo(email);

                HttpContext.Session.SetString("userSession", JsonConvert.SerializeObject(user));

                return RedirectToAction("Index", "Home");
            }
         catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();

        }


        public IActionResult LogOut(int id)
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }



        public async Task<ActionResult> CreateUserAuth(string email,string password)
        {
            try
            {
                UserCredential userCredential = await FireBaseAuthHelper.setFirebaseAuthClient().CreateUserWithEmailAndPasswordAsync(email, password);

                return RedirectToAction("Index", "Login");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }

        }



        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

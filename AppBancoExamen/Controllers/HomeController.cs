using AppBancoExamen.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AppBancoExamen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public UserModel GetSessionInfo() {
            try
            {
                if(!string.IsNullOrEmpty(HttpContext.Session.GetString("userSession")))
                {
                    UserModel? user = JsonConvert.DeserializeObject<UserModel>(HttpContext.Session.GetString("userSession"));

                    return user;
                }

                return null;
            }
            catch {
                return null;
            }

        }




        public async Task<IActionResult> Index()
        {
            UserModel? user = GetSessionInfo();
            if (user != null) {

                ViewBag.User = user;

                Cuenta cuenta = await CuentasHelper.GetCuentaInfo(user.Email);

                ViewBag.Cuenta = cuenta;


                

                return View();
            }
            return RedirectToAction("Index", "Error");
            
        }

        public IActionResult Privacy()
        {
            UserModel? user = GetSessionInfo();
            if (user != null)
            {
                ViewBag.User = user;


                List<Cuenta> CuentaList = CuentasHelper.GetCuentas(user.Email).Result;

                ViewBag.Cuenta = CuentaList;

                HttpContext.Session.SetString("CuentaList", JsonConvert.SerializeObject(CuentaList));


                return View();
            }
            return RedirectToAction("Index", "Error");
        }

        public IActionResult Transacciones()
        {

            UserModel? user = GetSessionInfo();
            if (user != null)
            {
                ViewBag.User = user;



                return View();


            }
            return RedirectToAction("Index", "Error");
        }

        //Guardo la transaccion 
        public IActionResult CreateTransacciones(int txtCantidad,string txtMotivo, int txtNumeroCuenta) {

            UserModel? user = GetSessionInfo();
            if (user != null)
            {
                ViewBag.User = user;

                bool result = TransaccionHelper.saveTransaccion(new TransaccionesModel
                {
                    Cantidad = Convert.ToInt32(txtCantidad),
                    Email = user.Email,
                    FechaIni = DateTime.Today.ToString(),
                    Motivo = txtMotivo,
                    NumeroCuenta = txtNumeroCuenta


                }).Result;
              
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("Index", "Error");
        }

        public IActionResult Historial()
        {

            UserModel? user = GetSessionInfo();
            if (user != null)
            {
                ViewBag.User = user;

                List<TransaccionesModel> transaccionesList = TransaccionHelper.GetTransacciones(user.Email).Result;

                ViewBag.TransaccionesModel = transaccionesList;

                HttpContext.Session.SetString("transaccionesList", JsonConvert.SerializeObject(transaccionesList));

                
                return View();
            }
            return RedirectToAction("Index", "Error");
        }

       



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

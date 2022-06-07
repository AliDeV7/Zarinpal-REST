using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Zarinpal_REST.Models;

namespace Zarinpal_REST.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;


        public HomeController(ILogger<HomeController> logger, IConfiguration _config)
        {
            _logger = logger;
            this._config = _config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GetBankLink()
        {
            var zarinpal = Models.ZarinPal.ZarinPal.Get();
            var Amount = 1000;

            var AppBaseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/Home/verify";
            String MerchantID = _config["ZarinPal:MerchantID"];
            String CallbackURL = $"{AppBaseUrl}?returnURL=https://google.com&userId=123123&Amount={Amount}";
            String Description = "تست درگاه بانک";

            //zarinpal.EnableSandboxMode();

            Models.ZarinPal.PaymentRequest pr = new Models.ZarinPal.PaymentRequest(MerchantID, Amount, CallbackURL, Description);

            var res = zarinpal.InvokePaymentRequest(pr);
            if (res.Status == 100)
                return Redirect(res.PaymentURL);
            else
                return View("Error");
        }

        public IActionResult Verify(string returnURL, string userId, long Amount, string Status = "NOK", string Authority = "0")
        {
            TempData["returnURL"] = returnURL;
            ViewData["status"] = Status;

            if (Authority != "0")
            {
                var zarinpal = Models.ZarinPal.ZarinPal.Get();
                String MerchantID = _config["ZarinPal:MerchantID"];
                var pv = new Models.ZarinPal.PaymentVerification(MerchantID, Amount, Authority);
                var verificationResponse = zarinpal.InvokePaymentVerification(pv);
                if (verificationResponse.Status != 100)
                    ViewData["status"] = "NOK";
            }

            if (Status != "OK")
                return View();

            return View();
        }
    }
}

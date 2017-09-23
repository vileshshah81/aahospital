using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aashrayhospital.com.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult UnderMaintenance()
        {
            return View();
        }

        [HttpPost]
        public void SendEmail(string recipient, string subject, string message)
        {
            Helper.EmailHelper.SendMail(subject, message, recipient);
        }

        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}
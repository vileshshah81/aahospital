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

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void Contact(string name, string email, string phone, string subject, string message)
        {
            string body = string.Format("Name:{0} <br> Email:{1} <br> Phone:{2} <br> Subject:{3} <br> Message:{4}", name,
                email, phone, subject, message);
            Helper.EmailHelper.SendMail(subject, body, "aashrayhospital@gmail.com", replyTo: email, replayToName: name);

            Helper.EmailHelper.SendMail("Thank you", "We will contact you back", email);
        }

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
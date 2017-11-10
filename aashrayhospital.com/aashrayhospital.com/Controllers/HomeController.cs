using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace aashrayhospital.com.Controllers
{
    using Helper;

    public class HomeController : Controller
    {
        public ActionResult UnderMaintenance()
        {
            return View();
        }

        [HttpPost]
        public void SendEmail(string recipient, string subject, string message)
        {
            EmailHelper.SendMail(subject, message, recipient);
        }

        public ActionResult Index()
        {
            if (MvcApplication.IsLive)
            {
                return View();
            }
            else
            {
                return RedirectToAction("UnderMaintenance", "Home");
            }
        }

        [HttpPost]
        public ActionResult Contact(string name, string email, string phone, string subject, string message)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    Task.Run(() =>
                    {
                        string body =
                            string.Format("Name:{0} <br> Email:{1} <br> Phone:{2} <br> Subject:{3} <br> Message:{4}",
                                name,
                                email, phone, subject, message);
                        EmailHelper.SendMail(subject, body, "aashrayhospital@gmail.com", replyTo: email,
                            replayToName: name);
                    });

                    Task.Run(() => EmailHelper.SendMail("Thank you", "We will contact you back", email));
                }
            }
            catch (Exception ex)
            {
                ex.Log(this);
            }
            return RedirectToAction("Index", "Home");
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
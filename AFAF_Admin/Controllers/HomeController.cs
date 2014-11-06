using System;
using System.Collections.Generic;
using System.Linq;
using AFAF_Admin.Models;
using System.Web;
using System.Web.Mvc;

namespace AFAF_Admin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                if (Session["user"] == null)
                {
                    return RedirectToAction("Login", "Users");
                }

                return View();
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult About()
        {
            try
            {
                ViewBag.Message = "Your app description page.";

                return View();
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult Contact()
        {
            try
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //GET Error
        public ActionResult Error()
        {
            return View();
        }
    }
}

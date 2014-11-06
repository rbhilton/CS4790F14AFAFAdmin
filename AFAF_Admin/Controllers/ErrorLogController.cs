using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AFAF_Admin.Models;

namespace AFAF_Admin.Controllers
{
    public class ErrorLogController : Controller
    {
        ErrorLogEntities db = new ErrorLogEntities();

        //
        // GET: /ErrorLog/
        public ActionResult Index()
        {
            IEnumerable<ErrorLog> errorLogList;

            try
            {
                errorLogList = db.ErrorLogs.ToList();
            }
            catch(Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }

            return View(errorLogList);
        }

        [HttpPost]
        public ActionResult Index(String testInt)
        {
            try
            {
                int i = 44 / Int32.Parse(testInt);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }


            return RedirectToAction("Index", "Home");
        }
    }
}

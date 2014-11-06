using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AFAF_Admin.Models;

namespace AFAF_Admin.Controllers
{
    public class EventsController : Controller
    {
        private AdminEntities db = new AdminEntities();

        //
        // GET: /Events/

        public ActionResult Index()
        {
            try
            {
                if (Session["user"] == null)
                {
                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    if (!adminPermission((string)Session["permission"]))
                    {
                        return RedirectToAction("Exception", "Users");
                    }
                }
                return View(db.Events.ToList());
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // GET: /Events/Details/5

        /*public ActionResult Details(int id = 0)
        {
            Event events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }*/

        //
        // GET: /Events/Create

        public ActionResult Create()
        {
            try
            {
                if (Session["user"] == null)
                {
                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    if (!adminPermission((string)Session["permission"]))
                    {
                        return RedirectToAction("Exception", "Users");
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // POST: /Events/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Event events)
        {
            try
            {
                if (Session["user"] == null)
                {
                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    if (!adminPermission((string)Session["permission"]))
                    {
                        return RedirectToAction("Exception", "Users");
                    }
                }

                if (ModelState.IsValid)
                {
                    events = TrimStringProperties(events);
                    db.Events.Add(events);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(events);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // GET: /Events/Edit/5

        public ActionResult Edit(int id = 0)
        {
            try
            {
                if (Session["user"] == null)
                {
                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    if (!adminPermission((string)Session["permission"]))
                    {
                        return RedirectToAction("Exception", "Users");
                    }
                }

                Event events = db.Events.Find(id);
                if (events == null)
                {
                    return HttpNotFound();
                }
                return View(events);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // POST: /Events/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Event events)
        {
            try
            {
                if (Session["user"] == null)
                {
                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    if (!adminPermission((string)Session["permission"]))
                    {
                        return RedirectToAction("Exception", "Users");
                    }
                }
                if (ModelState.IsValid)
                {
                    events = TrimStringProperties(events);
                    db.Entry(events).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(events);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // GET: /Events/Delete/5

        public ActionResult Delete(int id = 0)
        {
            try
            {
                if (Session["user"] == null)
                {
                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    if (!adminPermission((string)Session["permission"]))
                    {
                        return RedirectToAction("Exception", "Users");
                    }
                }
                Event events = db.Events.Find(id);
                if (events == null)
                {
                    return HttpNotFound();
                }
                return View(events);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // POST: /Events/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (Session["user"] == null)
                {
                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    if (!adminPermission((string)Session["permission"]))
                    {
                        return RedirectToAction("Exception", "Users");
                    }
                }
                Event events = db.Events.Find(id);
                db.Events.Remove(events);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                db.Dispose();
                base.Dispose(disposing);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                RedirectToAction("Error", "Home");
            }
        }

        public static bool adminPermission(string permission)
        {
            bool valid = false;

            try
            {
                if (permission.Substring(1, 1).Equals("1"))
                {
                    valid = true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
            }

            return valid;
        }

        public Event TrimStringProperties(Event input)
        {
            try
            {
                var stringProperties = input.GetType().GetProperties()
                    .Where(p => p.PropertyType == typeof(string));

                foreach (var stringProperty in stringProperties)
                {
                    string currentValue = (string)stringProperty.GetValue(input, null);
                    if (currentValue != null)
                        stringProperty.SetValue(input, currentValue.Trim(), null);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                RedirectToAction("Error", "Home");
            }

            return input;
        }
    }
}
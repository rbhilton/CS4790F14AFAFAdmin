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
    public class RecipientsController : Controller
    {
        private AdminEntities db = new AdminEntities();

        //
        // GET: /Recipients/

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
                return View(db.Recipients.ToList());
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // GET: /Recipients/Details/5

        public ActionResult Details(string id = null)
        {
            try
            {
                Recipient recipient = db.Recipients.Find(id);

                if (recipient == null)
                {
                    return HttpNotFound();
                }

                return View(recipient);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // GET: /Recipients/Create

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
        // POST: /Recipients/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Recipient recipient)
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
                    recipient = TrimStringProperties(recipient);
                    db.Recipients.Add(recipient);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(recipient);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // GET: /Recipients/Edit/5

        public ActionResult Edit(string id = null)
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

                Recipient recipient = db.Recipients.Find(id);

                if (recipient == null)
                {
                    return HttpNotFound();
                }

                return View(recipient);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // POST: /Recipients/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Recipient recipient)
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
                    recipient = TrimStringProperties(recipient);
                    db.Entry(recipient).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(recipient);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // GET: /Recipients/Delete/5

        public ActionResult Delete(string id = null)
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
                Recipient recipient = db.Recipients.Find(id);
                if (recipient == null)
                {
                    return HttpNotFound();
                }
                return View(recipient);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // POST: /Recipients/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
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

                Recipient recipient = db.Recipients.Find(id);
                db.Recipients.Remove(recipient);
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
                if (permission.Substring(2, 1).Equals("1"))
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

        public Recipient TrimStringProperties(Recipient input)
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
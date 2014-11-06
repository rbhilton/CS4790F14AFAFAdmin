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
    public class ParticipantsController : Controller
    {
        private AdminEntities db = new AdminEntities();

        //
        // GET: /Participants/

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
                return View(db.Participants.ToList());
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // GET: /Participants/Details/5

        public ActionResult Details(int id = 0)
        {
            try
            {
                Participant participant = db.Participants.Find(id);

                if (participant == null)
                {
                    return HttpNotFound();
                }

                return View(participant);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // GET: /Participants/Create

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
        // POST: /Participants/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Participant participant)
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
                    participant = TrimStringProperties(participant);
                    db.Participants.Add(participant);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(participant);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // GET: /Participants/Edit/5

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

                Participant participant = db.Participants.Find(id);

                if (participant == null)
                {
                    return HttpNotFound();
                }

                return View(participant);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // POST: /Participants/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Participant participant)
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
                    participant = TrimStringProperties(participant);
                    db.Entry(participant).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(participant);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // GET: /Participants/Delete/5

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

                Participant participant = db.Participants.Find(id);

                if (participant == null)
                {
                    return HttpNotFound();
                }

                return View(participant);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // POST: /Participants/Delete/5

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

                Participant participant = db.Participants.Find(id);
                db.Participants.Remove(participant);
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
            if (permission.Substring(3, 1).Equals("1"))
            {
                valid = true;
            }
            return valid;
        }

        public Participant TrimStringProperties(Participant input)
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using System.Web.Helpers;
using AFAF_Admin.Models;

namespace AFAF_Admin.Controllers
{
    public class UsersController : Controller
    {

        private AdminEntities db = new AdminEntities();

        //
        // GET: /Users/

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
                return View(db.Users.ToList());
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //GET Exception
        public ActionResult Exception() 
        {
            return View();
        }

        //
        // GET: /Users/Details/5

       /* public ActionResult Details(int id)
        {
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }*/

        public ActionResult Login() {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users model, string returnUrl)
        {
            try
            {
                ModelState["userType"] = new ModelState();
                ModelState["firstName"] = new ModelState();
                ModelState["lastName"] = new ModelState();

                if (ModelState.IsValid && validatePassword(model.email, model.password))
                {
                    var query = db.Users.First(b => b.email == model.email);
                    Session["user"] = query.firstName;
                    Session["permission"] = query.userType;
                    return RedirectToAction("Index", "Home");
                }

                // If we got this far, something failed, redisplay form
                ModelState.AddModelError("incorrectLogin", "**The user name or password provided is incorrect.");

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }
        //
        // POST: /Account/LogOff

        public ActionResult LogOff()
        {
            Session.Clear();

            return RedirectToAction("Login", "Users");
        }
        //
        // GET: /Users/Create
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
        // POST: /Users/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users users, bool adminCheckBox=false, bool eventsCheckBox=false, bool donationsCheckBox=false,
                                    bool participantsCheckBox=false, bool givingTreeCheckBox=false, string confirmPassword="")
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
                //userType is null, this will make the userType State validation to true since we are using checkboxes 
                if (users.userType == null)
                {
                    ModelState["userType"] = new ModelState();
                }
                //compare the permission checkboxes and insert values 1 or 0
                string userType = "";
                userType += (adminCheckBox) ? "1" : "0";
                userType += (eventsCheckBox) ? "1" : "0";
                userType += (donationsCheckBox) ? "1" : "0";
                userType += (participantsCheckBox) ? "1" : "0";
                userType += (givingTreeCheckBox) ? "1" : "0";
                users.userType = userType;

                if (userType.Equals("00000"))
                {
                    ModelState.AddModelError("userPermission", "Please check at least one permission for the user.");
                }

                if (confirmPassword == "")
                {
                    ModelState.AddModelError("confirmPassword", "Confirm password field is empty.");
                }
                else
                {
                    if (!users.password.Equals(confirmPassword))
                    {
                        ModelState.AddModelError("confirmPassword", "Confirm password field does not match with password field.");
                    }
                    else
                    {
                        if (ModelState.IsValid)
                        {
                            users = TrimStringProperties(users);
                            users.password = Encode(users.password);
                            db.Users.Add(users);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                }

                return View(users);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // GET: /Users/Edit/5

        public ActionResult Edit(int id )
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
                Users users = db.Users.Find(id);
                if (users == null)
                {
                    return HttpNotFound();
                }

                return View(users);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Users users, bool adminCheckBox = false, bool eventsCheckBox = false, bool donationsCheckBox = false,
                                    bool participantsCheckBox = false, bool givingTreeCheckBox = false)
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
                string userType = "";
                userType += (adminCheckBox) ? "1" : "0";
                userType += (eventsCheckBox) ? "1" : "0";
                userType += (donationsCheckBox) ? "1" : "0";
                userType += (participantsCheckBox) ? "1" : "0";
                userType += (givingTreeCheckBox) ? "1" : "0";
                users.userType = userType;

                if (ModelState.IsValid)
                {
                    AdminEntities db1 = new AdminEntities();
                    var query = db1.Users.Find(users.ID);
                    if (query.password != users.password)
                    {
                        users.password = Encode(users.password);
                    }
                    users = TrimStringProperties(users);
                    db.Entry(users).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(users);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // GET: /Users/Delete/5

        public ActionResult Delete(int id)
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
                Users users = db.Users.Find(id);
                if (users == null)
                {
                    return HttpNotFound();
                }
                return View(users);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
                return RedirectToAction("Error", "Home");
            }
        }

        //
        // POST: /Users/Delete/5

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
                Users users = db.Users.Find(id);
                db.Users.Remove(users);
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

        public static string Encode(string password)
        {
            try
            {
                return Crypto.HashPassword(password);
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
            }

            return password;
        }

        public static bool validatePassword(string email, string password)
        {
            string hashedPassword = "";

            AdminEntities db = new AdminEntities();
            try
            {
               var query = db.Users.First(b => b.email == email);
               if (query != null) {
                   hashedPassword = query.password;
               }
            }
            catch (Exception ex)
            {
                ErrorLog.logError(ex, "");
            }
            
            return Crypto.VerifyHashedPassword(hashedPassword, password);
        }

        public static bool adminPermission(string permission) 
        {
            bool valid = false;

            try
            {
                if (permission.Substring(0, 1).Equals("1"))
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

        public Users TrimStringProperties(Users input)
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
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using ITINDEXAU.Models;
using System;
//using WebMatrix.WebData;

namespace ITINDEXAU.Controllers
{
    public class UsersController : Controller
    {
        //
       // Class.CodeClass cc = new Class.CodeClass();
        // GET: /Users/
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        private bool IsValid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            bool IsValid = false;

            using (var db = new ITINDEXAU.Models.ITINDEX_DBEntities())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == email && u.Enabled == true && u.Activated == true);
                if (user != null)
                {
                    if (user.Password == crypto.Compute(password, user.Salt))
                    {
                        IsValid = true;
                    }
                }
            }
            return IsValid;
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Login(Login u)
        {
            if (IsValid(u.EmailId, u.Password))
            {
                //WebSecurity.Login(u.EmailId, u.Password);
                FormsAuthentication.SetAuthCookie(u.EmailId, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Login details are wrong.");
            }
            return View(u);           
           
        }
        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAccount(Register user)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    using (var db = new ITINDEXAU.Models.ITINDEX_DBEntities())
                    {
                        var test = db.Users.FirstOrDefault(u => u.Email == user.EmailId);
                        var test1 = db.Users.FirstOrDefault(u => u.Email == user.EmailId && u.Activated == true);
                        if (test == null)
                        {
                            var crypto = new SimpleCrypto.PBKDF2();
                            crypto.HashIterations = 51;
                            var encrypPass = crypto.Compute(user.Password);
                            var newUser = db.Users.Create();
                            string rdcode = "";
                            Random ccd = new Random();
                            rdcode = (ccd.Next(000000, 999999)).ToString();
                            string ip = "";
                            string hostName = Dns.GetHostName(); // Retrive the Name of HOST
                            Console.WriteLine(hostName);
                            // Get the IP
                            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                            ip = myIP;
                            newUser.Email = user.EmailId;
                            newUser.Password = encrypPass;
                            newUser.Salt = crypto.Salt;
                            newUser.FirstName = user.FirstName;
                            newUser.LastName = user.LastName;
                            newUser.OfficeNumber = "";
                            newUser.MobileNumber = "";
                            newUser.RegisteredDate = DateTime.Now;
                            newUser.RegisteredIP = ip.ToString(); ;
                            newUser.ResetPassword = false;
                            newUser.Activated = true;
                            newUser.AdminUser = true;
                            newUser.Enabled = true;
                            newUser.LastChange = null;
                            newUser.LastLogin = DateTime.Now;
                            newUser.LastLoginIP = ip.ToString();
                            newUser.VerificationCode = rdcode.ToString();
                            newUser.VerificationDate = null;
                            db.Users.Add(newUser);
                            db.SaveChanges();
                            // ..............Send Mail...............................
                            string sub = "IT Index Email Verification!!!";

                            string url = "http://localhost:34784/User/Active/" + Email.CryptorEngine.EncodeTo64(rdcode) + "";
                            string body = "Hi, You have successfully Registered" + "<a href=" + url + ">Click here To Register </a>";
                            if (Email.MyMail.SendMail(user.EmailId, sub, body) == "sent")
                            {
                                ViewBag.regmsg = "Success";
                            }
                            else
                            {
                                ViewBag.regmsg = "Try";
                            }
                        }
                        else if (test1 == null)
                        {
                            ViewBag.regmsg = "Already";
                        }
                        else
                        {
                            ViewBag.regmsg = "AlreadyExit";
                        }

                    }
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Data is not correct");
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            return View();
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(Forgot f)
        {
            if (ModelState.IsValid)
            {
               try
                {
                    using (ITINDEX_DBEntities ctx = new ITINDEX_DBEntities())
                    {
                        string rdcode = "";
                        Random ccd = new Random();
                        rdcode = (ccd.Next(000000, 999999)).ToString();
                        var test = ctx.Users.FirstOrDefault(u => u.Email == f.EmailAddress);
                        if (test != null)
                        {
                            User e = (from e1 in ctx.Users where e1.Email == f.EmailAddress select e1).First();
                            e.ResetPassword = true;
                            e.VerificationCode = rdcode;
                            ctx.SaveChanges();
                            string sub = "IT Index Email Verification!!!";
                            string url = "http://localhost:34784/User/Active/" + Email.CryptorEngine.EncodeTo64(rdcode) + "";
                            string body = "Hi, You have successfully Registered" + "<a href=" + url + ">Click here To Register </a>";
                            string mail=Email.MyMail.SendMail(f.EmailAddress, sub, body);
                            if (mail == "sent")
                            {
                                 ModelState.AddModelError("Data", "Send successfully");
                            }
                            else
                            {
                                ModelState.AddModelError("Data", mail);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("Data", "invalid Email ID");                           
                        }
                    }
               }
                catch
                {
                    ModelState.AddModelError("Data", "Error 404 not found");   
                }
               
            }
           
            return View();
          
        }
        [HttpGet]
        public ActionResult Settings()
        {
            ITINDEX_DBEntities db = new ITINDEX_DBEntities();
            string id = User.Identity.Name;
            User user = db.Users.FirstOrDefault(u => u.Email == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            Setting s = new Setting();
            s.FirstName = user.FirstName;
            s.LastName = user.LastName;
            s.OfficePhone = user.OfficeNumber;
            s.MobilePhone = user.MobileNumber;
            s.CurrentPassword = null;
            s.NewPassword = null;
            s.ConfirmPassword = null;
            s.ID = user.ID;
            return View(s);            
        }
        [HttpPost]
        public ActionResult Settings(Setting s)
        {
            if (ModelState.IsValid)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                crypto.HashIterations = 51;               
                using (ITINDEX_DBEntities ctx = new ITINDEX_DBEntities())
                {
                    User u = (from e1 in ctx.Users where e1.ID == s.ID select e1).First();
                    if (IsValid(u.Email, s.CurrentPassword) == true)
                    {
                        if (s.NewPassword != "" && s.NewPassword != null)
                        {
                            var encrypPass = crypto.Compute(s.NewPassword);
                            u.Password = encrypPass;
                            u.LastChange = DateTime.Now;
                            u.Salt = crypto.Salt;
                            u.FirstName = s.FirstName;
                            u.LastName = s.LastName;
                            u.LastName = s.LastName;
                            u.MobileNumber = s.MobilePhone;
                            u.OfficeNumber = s.OfficePhone;
                        }
                        else
                        {
                            u.FirstName = s.FirstName;
                            u.LastName = s.LastName;
                            u.LastName = s.LastName;
                            u.MobileNumber = s.MobilePhone;
                            u.OfficeNumber = s.OfficePhone;
                        }
                        ctx.SaveChanges();
                        ModelState.AddModelError("Data", "Update successfully");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Current Password is not correct");
                    }
                }                    
               
            }
            return View();
        }

        [HttpGet]
        public ActionResult ResetPassword(string id="")
        {
            Class.CodeClass cc = new Class.CodeClass();
            int ids = cc.ScalerReturnInt("select ID from Users where VerificationCode='" + Email.CryptorEngine.DecodeFrom64(id) + "' and ResetPassword='true'");
            ResetPassword rp = new ResetPassword();
            rp.ID = ids;
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(ResetPassword rp)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var crypto = new SimpleCrypto.PBKDF2();
                    crypto.HashIterations = 51;
                    var encrypPass = crypto.Compute(rp.Password);
                    using (ITINDEX_DBEntities ctx = new ITINDEX_DBEntities())
                    {
                        User e = (from e1 in ctx.Users where e1.ID == rp.ID select e1).First();
                        e.Password = encrypPass;
                        e.LastChange = DateTime.Now;
                        e.ResetPassword = false;
                        e.Salt = crypto.Salt;
                        ctx.SaveChanges();
                        ModelState.AddModelError("Data", "Change successfully");
                    }
                }
                catch
                {
                    ModelState.AddModelError("Data", "Error 404 not found");
                }
            }
            return View();
        }
        public ActionResult Active(string id = "")
        {
            string result = "";           
            string decrpt = Email.CryptorEngine.DecodeFrom64(id);
            try
            {
                ITINDEX_DBEntities db = new ITINDEX_DBEntities();
                User registration = db.Users.FirstOrDefault(u => u.VerificationCode == decrpt);           
                if (registration == null)
                {
                    // return HttpNotFound();
                    result = "Failed";
                }
                if (registration.Activated == true)
                {
                    result = "AlreadyActivated";
                }
                else
                {
                    registration.Activated = true;
                    registration.VerificationDate = DateTime.Now;
                    db.SaveChanges();
                    result = "Activated";
                }
            }
            catch
            {
                result = "Failed";
            }
            ViewBag.Result = result;
            return View();

        }
    }
}

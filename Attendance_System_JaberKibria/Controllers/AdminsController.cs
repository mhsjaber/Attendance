using Attendance_System_JaberKibria.Common;
using Attendance_System_JaberKibria.DAL;
using Attendance_System_JaberKibria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attendance_System_JaberKibria.Controllers
{
    public class AdminsController : Controller
    {
        private readonly ImpAdmins _admins;
        public AdminsController()
        {
            _admins = new ImpAdmins();
        }

        [HttpGet]
        public ActionResult Index()
        {

            var adminViewModel = new List<AdminViewModel>() { };

            foreach (var admin in _admins.GetAllAdmins())
            {
                adminViewModel.Add(SetupUtil.Convert(admin));
            }
            adminViewModel = adminViewModel.ToList();

            return View(adminViewModel);
        }

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    if (Session["AdminUsername"] != null && Session["AdminPassword"] != null)
        //    {
        //        Verify va = new Verify();
        //        bool valid = va.ValidAdmin(Session["AdminUsername"].ToString());
        //        if (valid)
        //        {
        //            AdminViewModel adminViewModel = new AdminViewModel();

        //            return View(adminViewModel);
        //        }
        //        else
        //        {
        //            Session.Abandon();
        //            return RedirectToAction("Login");
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Name,Password,Username")]Admin admin)
        //{
        //    if (Session["AdminUsername"] != null && Session["AdminPassword"] != null)
        //    {
        //        Verify va = new Verify();
        //        bool valid = va.ValidAdmin(Session["AdminUsername"].ToString());
        //        if (valid)
        //        {
        //            AdminViewModel adminViewModel = new AdminViewModel();
        //            adminViewModel.Password = admin.Password;
        //            adminViewModel.Name = admin.Name;
        //            adminViewModel.Username = admin.Username;
        //            try
        //            {
        //                Regex rgx = new Regex(@"[^a-zA-Z0-9\s.-]");
        //                admin.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(rgx.Replace(admin.Name, "").ToLower()).Trim();

        //                Regex r = new Regex("^[a-zA-Z0-9.]*$");
        //                admin.Username = admin.Username.ToLower().Trim();
        //                admin.CreateDate = DateTime.Now;
        //                admin.CreatedBy = Convert.ToInt32(Session["AdminId"]);

        //                if (ModelState.IsValid && r.IsMatch(admin.Username))
        //                {
        //                    admin.Password = PasswordHelper.ComputeHash(admin.Password, "SHA512", GetBytes("Jaber"));
        //                    db.Admins.Add(admin);
        //                    db.SaveChanges();
        //                    return RedirectToAction("Index");
        //                }
        //                else if (!r.IsMatch(admin.Username))
        //                {
        //                    ViewBag.Message = "Invalid username. Allows alphanumeric characters only.";
        //                    return View(adminViewModel);
        //                }
        //                else
        //                {
        //                    ViewBag.Message = "Error is insert";
        //                    return View(adminViewModel);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                ViewBag.Message = ex.Message;
        //                return View(adminViewModel);
        //            }
        //        }
        //        else
        //        {
        //            Session.Abandon();
        //            return RedirectToAction("Login");
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}

        //[HttpGet]
        //public ActionResult Login()
        //{
        //    AdminViewModel adminViewModel = new AdminViewModel();
        //    if (Session["AdminUsername"] != null && Session["AdminPassword"] != null)
        //    {
        //        Verify va = new Verify();
        //        bool valid = va.ValidAdmin(Session["AdminUsername"].ToString());
        //        if (valid)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            Session.Abandon();
        //            return View(adminViewModel);
        //        }
        //    }
        //    else
        //    {
        //        return View(adminViewModel);
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(Admin admin)
        //{
        //    if (Session["AdminUsername"] == null || Session["AdminPassword"] == null)
        //    {
        //        AdminViewModel adminViewModel = new AdminViewModel();
        //        adminViewModel.Password = admin.Password;
        //        adminViewModel.Name = admin.Name;

        //        var admin2 = db.Admins.Where(s => s.Username == admin.Username);
        //        if (admin2.Count() == 1)
        //        {
        //            if (PasswordHelper.VerifyHash(adminViewModel.Password, "SHA512", admin2.Single().Password))
        //            {
        //                Session["AdminUsername"] = admin2.Single().Username;
        //                Session["AdminPassword"] = admin2.Single().Password;
        //                Session["AdminId"] = admin2.Single().Id;
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                ViewBag.Message = "Username and password not matching!";
        //                return View(adminViewModel);
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Invalid account";
        //            return View(adminViewModel);
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
        private static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
using Attendance_System_JaberKibria.Common;
using Attendance_System_JaberKibria.DAL;
using Attendance_System_JaberKibria.Helpers;
using Attendance_System_JaberKibria.Models;
using Attendance_System_JaberKibria.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Attendance_System_JaberKibria.Controllers
{
    public class AdminsController : Controller
    {
        private readonly ImpAdmins _admins;
        private readonly ImpEmployees _employees;
        private AccessVerify _accessVerify;
        public AdminsController()
        {
            _admins = new ImpAdmins();
            _employees = new ImpEmployees();
            _accessVerify = new AccessVerify();
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (_accessVerify.AdminVerify())
            {
                var adminViewModel = new List<AdminViewModel>() { };

                foreach (var admin in _admins.GetAllAdmins())
                {
                    adminViewModel.Add(SetupUtil.Convert(admin));
                }
                adminViewModel = adminViewModel.ToList();
                return View(adminViewModel);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {

            if (_accessVerify.AdminVerify())
            {
                AdminViewModel adminViewModel = new AdminViewModel();

                return View(adminViewModel);
            }
            else
            {
                Session.Abandon();
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Password,Username")]Admin admin)
        {
            if (_accessVerify.AdminVerify())
            {
                AdminViewModel adminViewModel = new AdminViewModel();
                adminViewModel.Password = admin.Password;
                adminViewModel.Name = admin.Name;
                adminViewModel.Username = admin.Username;
                try
                {
                    Regex rgx = new Regex(@"[^a-zA-Z0-9\s.-]");
                    admin.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(rgx.Replace(admin.Name, "").ToLower()).Trim();

                    Regex r = new Regex("^[a-zA-Z0-9.]*$");
                    admin.Username = admin.Username.ToLower().Trim();

                    if (ModelState.IsValid && r.IsMatch(admin.Username))
                    {
                        admin.Password = PasswordHelper.ComputeHash(admin.Password, "SHA512", GetBytes("Jaber"));
                        var res = _admins.AddOrUpdateAdmin(admin);
                        if (res==1)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Message = (res == 2627) ? "<b>Error!</b> Username already used!" : "<b>Error!</b> Not performed";
                            return View(adminViewModel);
                        }
                    }
                    else if (!r.IsMatch(admin.Username))
                    {
                        ViewBag.Message = "Invalid username. Allows alphanumeric characters only.";
                        return View(adminViewModel);
                    }
                    else
                    {
                        ViewBag.Message = "Error is insert";
                        return View(adminViewModel);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(adminViewModel);
                }
            }
            else
            {
                Session.Abandon();
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            var adminViewModel = new AdminViewModel();
            if (_accessVerify.AdminVerify())
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session.Abandon();
                return View(adminViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin admin)
        {
            if (!_accessVerify.AdminVerify())
            {
                var adminViewModel = new AdminViewModel(); 
                adminViewModel.Username = admin.Username;
                adminViewModel.Password = admin.Password;

                var user = _admins.Login(admin.Username);
                try
                {
                    if (PasswordHelper.VerifyHash(admin.Password, "SHA512", user.First().Password))
                    {
                        Session["AdminUsername"] = user.First().Username;
                        Session["AdminName"] = user.First().Username;
                        Session["AdminPassword"] = user.First().Password;
                        Session["AdminId"] = user.First().Id;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "<b>Error!</b> Username and password not matching!";
                        return View(adminViewModel);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(adminViewModel);
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

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
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
    public class AEmployeeController : Controller
    {
        private readonly ImpEmployees _employees;
        private AccessVerify _accessVerify;
        public AEmployeeController()
        {
            _employees = new ImpEmployees();
            _accessVerify = new AccessVerify();
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (_accessVerify.AdminVerify())
            {
                var employeeViewModel = new List<EmployeeViewModel>() { };

                foreach (var employee in _employees.GetAllEmployees())
                {
                    employeeViewModel.Add(SetupUtil.Convert(employee));
                }
                employeeViewModel = employeeViewModel.ToList();
                return View(employeeViewModel);
            }
            else
            {
                Session.Abandon();
                return RedirectToAction("Login", "Admins");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {

            if (_accessVerify.AdminVerify())
            {
                EmployeeViewModel adminViewModel = new EmployeeViewModel();

                return View(adminViewModel);
            }
            else
            {
                Session.Abandon();
                return RedirectToAction("Login", "Admins");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Password,Username")]Employee employee)
        {
            if (_accessVerify.AdminVerify())
            {
                EmployeeViewModel employeeViewModel = new EmployeeViewModel();
                employeeViewModel.Password = employee.Password;
                employeeViewModel.Name = employee.Name;
                employeeViewModel.Username = employee.Username;
                try
                {
                    Regex rgx = new Regex(@"[^a-zA-Z0-9\s.-]");
                    employee.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(rgx.Replace(employee.Name, "").ToLower()).Trim();

                    Regex r = new Regex("^[a-zA-Z0-9.]*$");
                    employee.Username = employee.Username.ToLower().Trim();

                    if (ModelState.IsValid && r.IsMatch(employee.Username))
                    {
                        employee.Password = PasswordHelper.ComputeHash(employee.Password, "SHA512", GetBytes("Jaber"));
                        var res = _employees.AddOrUpdateEmployee(employee);
                        if (res == 1)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Message = (res == 2627) ? "<b>Error!</b> Username already used!" : "<b>Error!</b> Not performed";
                            return View(employeeViewModel);
                        }
                    }
                    else if (!r.IsMatch(employee.Username))
                    {
                        ViewBag.Message = "Invalid username. Allows alphanumeric characters only.";
                        return View(employeeViewModel);
                    }
                    else
                    {
                        ViewBag.Message = "Error is insert";
                        return View(employeeViewModel);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(employeeViewModel);
                }
            }
            else
            {
                Session.Abandon();
                return RedirectToAction("Login", "Admins");
            }
        }

        private static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
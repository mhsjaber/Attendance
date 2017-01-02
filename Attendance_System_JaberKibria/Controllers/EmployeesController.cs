
using Attendance_System_JaberKibria.Common;
using Attendance_System_JaberKibria.DAL;
using Attendance_System_JaberKibria.Helpers;
using Attendance_System_JaberKibria.Models;
using Attendance_System_JaberKibria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attendance_System_JaberKibria.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ImpAdmins _admins;
        private readonly ImpAttendance _attendance;
        private readonly ImpEmployees _employee;
        private AccessVerify _accessVerify;
        private int _empId;

        public EmployeesController()
        {
            _admins = new ImpAdmins();
            _attendance = new ImpAttendance();
            _employee = new ImpEmployees();
            _accessVerify = new AccessVerify();
        }

        public ActionResult Index()
        {
            if (_accessVerify.EmployeeVerify())
            {
                var attendanceViewModel = new AttendanceViewModel();
                _empId = Convert.ToInt32(Session["Id"]);
                var res = _attendance.AttendanceDetails(_empId, DateTime.Today);

                ViewBag.Message = (TempData["Messahe"] != null) ? TempData["Message"].ToString() : "";

                if (res.Count() == 0)
                {
                    ViewData["Count"] = "0";
                }
                else
                {
                    ViewData["Count"] = "1";
                    attendanceViewModel.InTime = res.FirstOrDefault().InTime;
                    attendanceViewModel.OutTime = res.FirstOrDefault().OutTime;
                }
                return View(attendanceViewModel);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            var employeeViewModel = new EmployeeViewModel();
            if (_accessVerify.EmployeeVerify())
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session.Abandon();
                return View(employeeViewModel);
            }
        }


        [HttpPost]
        public ActionResult CheckIn()
        {
            if (_accessVerify.AdminVerify())
            {
                _empId = Convert.ToInt32(Session["Id"]);
                _attendance.Checkin(_empId);
                TempData["Message"] = (_attendance.Checkout(_empId) == 1) ? "Check in time submitted successfuly." : " <b>Error!</b> Not submitted.";
                return RedirectToAction("Index");
            }
            else
            {
                Session.Abandon();
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult CheckOut()
        {
            if (_accessVerify.AdminVerify())
            {
                _empId = Convert.ToInt32(Session["Id"]);
                TempData["Message"] = (_attendance.Checkout(_empId)==1)? "Check out time submitted successfuly.":" <b>Error!</b> Not submitted.";
                return RedirectToAction("Index");
            }
            else
            {
                Session.Abandon();
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Employee employee)
        {
            if (!_accessVerify.EmployeeVerify())
            {
                var employeeViewModel = new EmployeeViewModel();
                employeeViewModel.Username = employee.Username;
                employeeViewModel.Password = employee.Password;

                var user = _employee.Login(employee.Username);
                try
                {
                    if (PasswordHelper.VerifyHash(employee.Password, "SHA512", user.First().Password))
                    {
                        Session["Username"] = user.First().Username;
                        Session["Password"] = user.First().Password;
                        Session["Name"] = user.First().Name;
                        Session["Id"] = user.First().Id;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "<b>Error!</b> Username and password not matching!";
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
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}
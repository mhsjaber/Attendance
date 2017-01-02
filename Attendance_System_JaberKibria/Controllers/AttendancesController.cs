using Attendance_System_JaberKibria.Common;
using Attendance_System_JaberKibria.CustomModels;
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
    public class AttendancesController : Controller
    {
        private readonly ImpEmployees _employees;
        private readonly ImpAttendance _attendances;
        private AccessVerify _accessVerify;
        public AttendancesController()
        {
            _employees = new ImpEmployees();
            _attendances = new ImpAttendance();
            _accessVerify = new AccessVerify();
        }

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Createday()
        {

            if (_accessVerify.AdminVerify())
            {
                var attendanceViewModel = new AttendanceViewModel();
                attendanceViewModel.Date = DateTime.Today;
                return View(attendanceViewModel);
            }
            else
            {
                Session.Abandon();
                return RedirectToAction("Login", "Admins");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Createday(Attendance attendance)
        {
            if (_accessVerify.AdminVerify())
            {
                var attendanceViewModel = new AttendanceViewModel();
                attendanceViewModel.Date = attendance.Date;
                try
                {
                    var res = _attendances.Createday(_employees.GetAllEmployees(), attendance.Date);
                    if (res == "1")
                    {
                        ViewBag.Message = "Created successfully!";
                    }
                    else if(res== "2627")
                    {
                        ViewBag.Message = "Date already created.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
                return View(attendanceViewModel);
            }
            else
            {
                Session.Abandon();
                return RedirectToAction("Login", "Admins");
            }
        }

        [HttpGet]
        public ActionResult Daywise()
        {
            if (_accessVerify.AdminVerify())
            {
                return View();
            }
            else
            {
                Session.Abandon();
                return RedirectToAction("Login", "Admins");
            }
        }

        [HttpPost]
        public JsonResult Daywise(DateTime Date)
        {
            if (_accessVerify.AdminVerify())
            {
                var attendances = new List<AttendanceViewModel>() { };

                foreach (var att in _attendances.DaywiseData(Date))
                {
                    attendances.Add(SetupUtil.Convert(att));
                }

                return Json(attendances);
            }
            else
            {
                Session.Abandon();
                return null;
            }
        }



        [HttpGet]
        public ActionResult Personwise()
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

        [HttpPost]
        public JsonResult Personwise(int Id, int Month, int Year)
        {
            if (_accessVerify.AdminVerify())
            {
                var attendances = new List<AttendanceViewModel>() { };

                foreach (var att in _attendances.PersonwiseData(Id, Month, Year))
                {
                    attendances.Add(SetupUtil.Convert(att));
                }

                return Json(attendances);
            }
            else
            {
                Session.Abandon();
                return null;
            }
        }

        [HttpGet]
        public ActionResult UpdateTime()
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


        [HttpPost]
        public JsonResult GetDetails(int Id, DateTime Date)
        {
            if (_accessVerify.AdminVerify())
            {
                try
                {
                    var attendanceViewModel = new List<AttendanceViewModel>() { };

                    foreach (var att in _attendances.AttendanceDetails(Id, Date))
                    {
                        attendanceViewModel.Add(SetupUtil.Convert(att));
                    }
                    return Json(attendanceViewModel);
                }
                catch (Exception ex)
                {
                    return Json("");
                }
            }
            else
            {
                Session.Abandon();
                return Json("");
            }
        }

        [HttpPost]
        public JsonResult UpdateDetails(Attendance attendance)
        {
            if (_accessVerify.AdminVerify())
            {
                try
                {   
                    var attendanceCustom = SetupUtil.Convert(attendance);
                    return Json(_attendances.UpdateAttendance(attendanceCustom));
                }
                catch (Exception ex)
                {
                    return Json("");
                }
            }
            else
            {
                Session.Abandon();
                return Json("");
            }
        }


    }
}
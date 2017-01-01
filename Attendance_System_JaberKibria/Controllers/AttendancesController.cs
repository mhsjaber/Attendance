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
        // GET: Attendances
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Createday()
        {

            if (_accessVerify.AdminVerify())
            {
                AttendanceViewModel attendanceViewModel = new AttendanceViewModel();

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
                AttendanceViewModel attendanceViewModel = new AttendanceViewModel();
                attendanceViewModel.Date = attendance.Date;
                try
                {
                    var res = _attendances.Createday(_employees.GetAllEmployees(), attendance.Date);
                    if (res == "1")
                    {
                        ViewBag.Message = "Created successfully!";
                    }
                    else
                    {
                        ViewBag.Message = res;
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

    }
}
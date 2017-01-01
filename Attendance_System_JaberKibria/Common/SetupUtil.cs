using Attendance_System_JaberKibria.CustomModels;
using Attendance_System_JaberKibria.Models;
using Attendance_System_JaberKibria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance_System_JaberKibria.Common
{
    public static class SetupUtil
    {
        public static AdminViewModel Convert(AdminCustom admin)
        {
            try
            {
                var adminVM = new AdminViewModel();
                adminVM.Id = admin.Id;
                adminVM.Name = admin.Name;
                adminVM.Username = admin.Username;
                adminVM.Status = admin.Status;
                adminVM.CreateDate = admin.CreateDate;
                adminVM.UpdateDate = admin.UpdateDate;
                adminVM.UpdatedBy = admin.UpatedBy;
                adminVM.CreatedBy = admin.CreatedBy;
                adminVM.UpdatedAdmin = admin.UpdatedAdmin;
                adminVM.CreatedAdmin = admin.CreatedAdmin;

                return adminVM;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Admin Convert(AdminViewModel adminVM)
        {
            try
            {
                var admin = new Admin();
                admin.Id = adminVM.Id;
                admin.Name = adminVM.Name;
                admin.Password = adminVM.Password;
                admin.Username = adminVM.Username;
                admin.Status = adminVM.Status;
                admin.CreateDate = adminVM.CreateDate;
                admin.CreatedBy = adminVM.CreatedBy;
                admin.UpdateDate = adminVM.UpdateDate;
                admin.UpatedBy = adminVM.UpdatedBy;

                return admin;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static EmployeeViewModel Convert(EmployeeCustom employeeCustom)
        {
            try
            {
                var employeeVM = new EmployeeViewModel();
                employeeVM.Id = employeeCustom.Id;
                employeeVM.Name = employeeCustom.Name;
                employeeVM.Username = employeeCustom.Username;
                employeeVM.Status = employeeCustom.Status;
                employeeVM.CreateDate = employeeCustom.CreateDate;
                employeeVM.CreatedAdmin = employeeCustom.CreatedAdmin;
                employeeVM.UpdateDate = employeeCustom.CreateDate;
                employeeVM.UpdatedAdmin = employeeCustom.UpdatedAdmin;

                return employeeVM;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Employee Convert(EmployeeViewModel employeeVM)
        {
            try
            {
                var employee = new Employee();
                employee.Id = employeeVM.Id;
                employee.Name = employeeVM.Name;
                employee.Password = employeeVM.Password;
                employee.Username = employeeVM.Username;
                employee.Status = employeeVM.Status;
                employee.CreateDate = employeeVM.CreateDate;
                employee.CreatedBy = employeeVM.CreatedBy;
                employee.UpdateDate = employeeVM.UpdateDate;
                employee.UpdatedBy = employeeVM.UpdatedBy;

                return employee;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static AttendanceViewModel Convert(Attendance attendance)
        {
            try
            {
                var attendanceVM = new AttendanceViewModel();
                attendanceVM.Id = attendance.Id;
                attendanceVM.InTime = attendance.InTime;
                attendanceVM.OutTime = attendance.OutTime;
                attendanceVM.UpdateDate = attendance.UpdateDate;
                attendanceVM.UpdatedAdmin = attendance.AdminUpdateBy.Username;
                attendanceVM.UpdatedBy = attendance.UpdatedBy;
                attendanceVM.CreateDate = attendance.CreateDate;
                attendanceVM.CreatedAdmin = attendance.AdminCreatedBy.Username;
                attendanceVM.CreatedBy = attendance.CreatedBy;
                attendanceVM.Date = attendance.Date;
                attendanceVM.EmployeeName = attendance.Employee.Name;
                attendanceVM.EmployeeId = attendance.EmployeeId;

                return attendanceVM;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Attendance Convert(AttendanceViewModel attendanceVM)
        {
            try
            {
                var attendance = new Attendance();
                attendance.Id = attendanceVM.Id;
                attendance.InTime = attendanceVM.InTime;
                attendance.OutTime = attendanceVM.OutTime;
                attendance.UpdateDate = attendanceVM.UpdateDate;
                attendance.UpdatedBy = attendanceVM.UpdatedBy;
                attendance.CreateDate = attendanceVM.CreateDate;
                attendance.CreatedBy = attendanceVM.CreatedBy;
                attendance.Date = attendanceVM.Date;
                attendance.EmployeeId = attendanceVM.EmployeeId;

                return attendance;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
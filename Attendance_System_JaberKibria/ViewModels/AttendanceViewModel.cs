using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance_System_JaberKibria.ViewModels
{
    public class AttendanceViewModel
    {
        public string CreatedAdmin { get; internal set; }
        public DateTime CreateDate { get; internal set; }
        public int CreatedBy { get; internal set; }
        public DateTime Date { get; internal set; }
        public int EmployeeId { get; internal set; }
        public string EmployeeName { get; internal set; }
        public int Id { get; internal set; }
        public DateTime? InTime { get; internal set; }
        public DateTime? OutTime { get; internal set; }
        public string UpdatedAdmin { get; internal set; }
        public DateTime? UpdateDate { get; internal set; }
        public int? UpdatedBy { get; internal set; }
    }
}
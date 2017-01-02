using Attendance_System_JaberKibria.CustomModels;
using Attendance_System_JaberKibria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance_System_JaberKibria.DAL
{
    interface IAttendance
    {
        string Createday(IEnumerable<EmployeeCustom> employees, DateTime Date);
        IEnumerable<AttendanceCustom> DaywiseData(DateTime Date);
        IEnumerable<AttendanceCustom> PersonwiseData(int Id, int Month, int Year);
        IEnumerable<AttendanceCustom> AttendanceDetails(int Id, DateTime Date);
        int UpdateAttendance(AttendanceCustom attendance);
        int Checkin(int id);
        int Checkout(int id);
    }
}

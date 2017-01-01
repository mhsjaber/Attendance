using Attendance_System_JaberKibria.CustomModels;
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
    }
}

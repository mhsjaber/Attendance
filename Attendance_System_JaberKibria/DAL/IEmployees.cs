using Attendance_System_JaberKibria.CustomModels;
using Attendance_System_JaberKibria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance_System_JaberKibria.DAL
{
    interface IEmployees
    {
        IEnumerable<EmployeeCustom> GetAllEmployees();
        int AddOrUpdateEmployee(Employee employee);
        bool DeleteEmployee(int Id);
        IEnumerable<Employee> Login(string Username);
    }
}

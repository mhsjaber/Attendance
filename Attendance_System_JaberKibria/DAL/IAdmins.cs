using Attendance_System_JaberKibria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance_System_JaberKibria.DAL
{
    interface IAdmins
    {
        IEnumerable<Admin> GetAllAdmins();
        bool AddOrUpdateAdmin(Admin admin);
        bool DeleteAdmin(int Id);
    }
}

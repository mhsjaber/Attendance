using Attendance_System_JaberKibria.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance_System_JaberKibria.Helpers
{
    public class AccessVerify
    {
        public bool AdminVerify()
        {
            if (HttpContext.Current.Session["AdminId"] != null && HttpContext.Current.Session["AdminName"] != null && HttpContext.Current.Session["AdminUsername"] != null)
            {
                var admin = new ImpAdmins();
                var admins = admin.Login(HttpContext.Current.Session["AdminUsername"].ToString());
                if (admins.Count() == 1) { return true; }
                else { return false; }
                
            }
            else
            {
                return false;
            }
        }

        internal bool EmployeeVerify()
        {
            if (HttpContext.Current.Session["Id"] != null && HttpContext.Current.Session["Name"] != null && HttpContext.Current.Session["Username"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
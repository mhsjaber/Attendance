﻿using System.Web;
using System.Web.Mvc;

namespace Attendance_System_JaberKibria
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

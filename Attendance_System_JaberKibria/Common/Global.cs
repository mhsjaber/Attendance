using Attendance_System_JaberKibria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Attendance_System_JaberKibria.Common
{
    public class Global
    {
        private static AttendanceSystemEntities _dbContext;
        private static string _connectionString;

        public static AttendanceSystemEntities dbContext
        {
            get
            {
                if (_dbContext == null) _dbContext = new AttendanceSystemEntities();
                return _dbContext;
            }
        }
        public static string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                {
                    _connectionString = @"data source=JABER\SQLEXPRESS;initial catalog=AttendanceSystem;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                }
                return _connectionString;
            }
        }
        
    }
}
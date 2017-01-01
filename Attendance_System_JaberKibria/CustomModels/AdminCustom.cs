using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance_System_JaberKibria.CustomModels
{
    public class AdminCustom
    {
        public string CreatedAdmin { get; internal set; }
        public DateTime CreateDate { get; internal set; }
        public int? CreatedBy { get; internal set; }
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public int Status { get; internal set; }
        public int? UpatedBy { get; internal set; }
        public string UpdatedAdmin { get; internal set; }
        public DateTime? UpdateDate { get; internal set; }
        public string Username { get; internal set; }
    }
}
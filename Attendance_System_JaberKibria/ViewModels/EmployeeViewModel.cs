﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attendance_System_JaberKibria.ViewModels
{
    public class EmployeeViewModel
    {
        [Display(Name = "Created By")]
        public string CreatedAdmin { get; internal set; }
        [Display(Name ="Create Date")]
        public DateTime CreateDate { get; internal set; }
        public int CreatedBy { get; internal set; }
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public string Password { get; internal set; }
        public int Status { get; internal set; }
        public int? UpdatedBy { get; internal set; }
        [Display(Name = "Updated By")]
        public string UpdatedAdmin { get; internal set; }
        [Display(Name = "Update Date")]
        public DateTime? UpdateDate { get; internal set; }
        public string Username { get; internal set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Utility
{
    public class AppConstant
    {
        public const string SuperAdminRole = "SuperAdminRole";
        public const string StudentRole = "StudentRole";
        public const string LecturerRole = "LecturerRole";
    }

    public class AppSettings
    {
        public string SuperAdminEmail { get; set; }
        public string SuperAdminPassword { get; set; }
    }

}

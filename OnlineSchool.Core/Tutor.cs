using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Core
{
    public class Tutor : Entity
    {
        public string Fullname { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}

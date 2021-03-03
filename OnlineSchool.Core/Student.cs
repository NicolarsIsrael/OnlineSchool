using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Core
{
    public class Student : Entity
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string MatricNumber { get; set; }
        public string ProfilePicturePath { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}

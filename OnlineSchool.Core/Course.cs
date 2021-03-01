using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Core
{
    public class Course : Entity
    {
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }

        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }
        public ICollection<Student> Students { get; set; }
        //public ICollection<Lecture> Lectures { get; set; }
    }
}

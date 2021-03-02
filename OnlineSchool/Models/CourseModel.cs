using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSchool.Models
{
    public class ViewCourseModel
    {
        public int Id { get; set; }
        [Display(Name ="Course title")]
        public string CourseTitle { get; set; }
        [Display(Name = "Course code")]
        public string CourseCode { get; set; }
        [Display(Name = "Description")]
        public string CourseDescription { get; set; }
        [Display(Name = "Tutor")]
        public string TutorName { get; set; }
        public ViewCourseModel(Course course)
        {
            Id = course.Id;
            CourseTitle = course.CourseTitle;
            CourseCode = course.CourseCode;
            CourseDescription = course.Description;
            TutorName = course.Tutor.Fullname;
        }
    }

    public class AddCourseViewModel
    {
        [Required(ErrorMessage ="Course code is required")]
        [Display(Name = "Course title")]
        [RegularExpression("^[a-zA-Z0-9].*$", ErrorMessage = "Course should begin with letter or number")]
        public string CourseTitle { get; set; }

        [Required(ErrorMessage = "Course code is required")]
        [Display(Name = "Course code")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Invalid course code")]
        [Remote("UniqueCourseCode", "Admin", ErrorMessage = "Course code already exists")]
        public string CourseCode { get; set; }

        [Display(Name = "Description")]
        public string CourseDescription { get; set; }
        public int SelectedTutor { get; set; }
        public List<SelectListItem> Tutors { get; set; }
        public AddCourseViewModel(IEnumerable<Tutor> tutors)
        {
            tutors = tutors.OrderBy(t => t.Fullname);
            var tutorsList = new List<SelectListItem>();
            foreach (var t in tutors)
                tutorsList.Add(new SelectListItem(t.Fullname, t.Id.ToString()));
            Tutors = tutorsList;
        }

        public AddCourseViewModel()
        {

        }
        public Course Add(Tutor tutor)
        {
            return new Course
            {
                CourseCode = CourseCode,
                CourseTitle = CourseTitle,
                Description = CourseDescription,
                Tutor = tutor,
            };
        }
    }
}

using Microsoft.AspNetCore.Http;
using OnlineSchool.Core;
using OnlineSchool.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSchool.Models
{
    public class ViewLectureModel
    {
        public string Title { get; set; }
        public string BriefDescription { get; set; }
        public string FilePath { get; set; }
        public int CourseId { get; set; }
        public ViewLectureModel(Lecture lecture)
        {
            Title = lecture.Title;
            FilePath = string.IsNullOrEmpty(lecture.FilePath) ? "/default-course-img.jpg" : GeneralFunction.GetUrlPath(lecture.FilePath);
            BriefDescription = lecture.BriefDescription;
            CourseId = lecture.CourseId;
        }
    }

    public class AddLectureModel
    {
        public int CourseId { get; set; }

        public string CourseCode { get; set; }

        [Required(ErrorMessage ="Title is required")]
        [RegularExpression("^[a-zA-Z0-9].*$", ErrorMessage = "Title should begin with letter or number")]
        public string Title { get; set; }

        [RegularExpression("^[a-zA-Z0-9].*$", ErrorMessage = "Brief should begin with letter or number")]
        public string BriefDescription { get; set; }

        [Required(ErrorMessage ="Lecture file is required")]
        public IFormFile File { get; set; }
        public AddLectureModel() { }
        public AddLectureModel(int courseId, string courseCode)
        {
            CourseId = courseId;
            CourseCode = courseCode;
        }
        public Lecture Add(string filePath, Course course)
        {
            return new Lecture
            {
                Title = Title,
                BriefDescription = BriefDescription,
                FilePath = filePath,
                Course = course,
            };
        }
    }   
}

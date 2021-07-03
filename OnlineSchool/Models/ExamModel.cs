using OnlineSchool.Core;
using OnlineSchool.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSchool.Models
{
    public class ViewExamModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal TotalScore { get; set; }
        public decimal CoursePercentage { get; set; }
        public int DurationInMinutes { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }
        public string StartTime { get; set; }
        public string DeadlineStartTime { get; set; }
        public string DeadlineEndTime { get; set; }
        public IEnumerable<ViewMcqQuestion> McqQuestions { get; set; }
        public ViewExamModel(Exam exam)
        {
            Id = exam.Id;
            Title = exam.ExamTitle;
            CoursePercentage = exam.CoursePerentage;
            DurationInMinutes = exam.DurationInMinute;
            CourseTitle = exam.Course.CourseTitle;
            CourseCode = exam.Course.CourseCode;
            StartTime = GeneralFunction.DateInString(exam.StartTime);
            DeadlineEndTime = GeneralFunction.DateInString(exam.DeadlineEndTime);
            DeadlineStartTime = GeneralFunction.DateInString(exam.DeadlineStartTime);
            TotalScore = TotalScore;
            McqQuestions = exam.MultiChoiceQuestions.Select(mc => new ViewMcqQuestion(mc,true));
        }
    }

    public class NewExamModel
    {
        [RegularExpression("^[a-zA-Z0-9].*$", ErrorMessage = "Title should begin with letter or number")]
        [Required(ErrorMessage ="Title is required")]
        public string Title { get; set; }

        [Display(Name ="Course percentage")]
        [Range(0,100,ErrorMessage ="Course percentage should be between 0 and 100")]
        [Required(ErrorMessage = "Course percentage is required")]
        public decimal CoursePerentage { get; set; }

        [Display(Name ="Duration in minute")]
        [Range(0,int.MaxValue,ErrorMessage ="Duration can not be negative")]
        [Required(ErrorMessage = "Duration is required")]
        public int DurationInMinute { get; set; }

        public int CourseId { get; set; }

        [Display(Name ="Start time")]
        [Required(ErrorMessage = "Start time is required")]
        public DateTime StartTime { get; set; }

        [Display(Name ="Deadline start time")]
        [Required(ErrorMessage = "Deadline start time is required")]
        public DateTime DeadlineStartTime { get; set; }

        [Display(Name ="Deadline end time")]
        [Required(ErrorMessage = "Deadline end time is required")]
        public DateTime DeadlineEndTime { get; set; }

        public NewExamModel()
        {

        }

        public NewExamModel(int courseId)
        {
            CourseId = courseId;
            StartTime = DateTime.Now;
            DeadlineEndTime = DateTime.Now;
            DeadlineStartTime = DateTime.Now;
        }

        public Exam Add(Course course)
        {
            return new Exam
            {
                ExamTitle = Title,
                TotalScore = 0,
                CoursePerentage = CoursePerentage,
                DurationInMinute = DurationInMinute,
                Course = course,
                StartTime = StartTime,
                DeadlineStartTime = DeadlineStartTime,
                DeadlineEndTime = DeadlineEndTime,
            };
        }
    }
    public class EditExamModel
    {
        public int ExamId { get; set; }

        [RegularExpression("^[a-zA-Z0-9].*$", ErrorMessage = "Title should begin with letter or number")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Display(Name = "Course percentage")]
        [Range(0, 100, ErrorMessage = "Course percentage should be between 0 and 100")]
        [Required(ErrorMessage = "Course percentage is required")]
        public decimal CoursePerentage { get; set; }

        [Display(Name = "Duration in minute")]
        [Range(0, int.MaxValue, ErrorMessage = "Duration can not be negative")]
        [Required(ErrorMessage = "Duration is required")]
        public int DurationInMinute { get; set; }

        public int CourseId { get; set; }

        [Display(Name = "Start time")]
        [Required(ErrorMessage = "Start time is required")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Deadline start time")]
        [Required(ErrorMessage = "Deadline start time is required")]
        public DateTime DeadlineStartTime { get; set; }

        [Display(Name = "Deadline end time")]
        [Required(ErrorMessage = "Deadline end time is required")]
        public DateTime DeadlineEndTime { get; set; }

        public EditExamModel()
        {

        }

        public EditExamModel(Exam exam)
        {
            ExamId = exam.Id;
            Title = exam.ExamTitle;
            CoursePerentage = exam.CoursePerentage;
            DurationInMinute = exam.DurationInMinute;
            StartTime = exam.StartTime;
            DeadlineEndTime = exam.DeadlineEndTime;
            DeadlineStartTime = exam.DeadlineStartTime;
            CourseId = exam.Course.Id;
        }

        public Exam Edit(Exam exam)
        {
            exam.ExamTitle = Title;
            exam.CoursePerentage = CoursePerentage;
            exam.DurationInMinute = DurationInMinute;
            exam.StartTime = StartTime;
            exam.DeadlineEndTime = DeadlineEndTime;
            exam.DeadlineStartTime = DeadlineStartTime;

            return exam;
        }
    }
}

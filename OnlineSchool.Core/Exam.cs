using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Core
{
    public class Exam : Entity
    {
        public string ExamTitle { get; set; }
        public int TotalScore { get; set; }
        public decimal CoursePerentage { get; set; }
        public int DurationInMinute { get; set; }
        public Course Course { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime DeadlineStartTime { get; set; }
        public DateTime DeadlineEndTime { get; set; }
        public IEnumerable<McqQuestion> MultiChoiceQuestions { get; set; }
    }
}

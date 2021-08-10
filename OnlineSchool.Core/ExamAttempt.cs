using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Core
{
    public class ExamAttempt : Entity
    {
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public int DurationInSeconds { get; set; }
        public IEnumerable<ExamMcqAttempt> Mcqs { get; set; }
        public bool ContinueAttempt { get; set; }
        public decimal Score { get; set; }
        public decimal MaximumScore { get; set; }
    }
}

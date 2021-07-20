using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Core
{
    public class ExamAttempt : Entity
    {
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int DurationInSeconds { get; set; }
        public IEnumerable<ExamMcqAttempt> Mcqs { get; set; }
        public bool ContinueAttempt { get; set; }
    }
}

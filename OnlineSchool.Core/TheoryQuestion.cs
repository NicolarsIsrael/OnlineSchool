using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Core
{
    public class TheoryQuestion : Entity
    {
        public string Question { get; set; }
        public string QuestionFile { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public decimal Score { get; set; }
    }
}

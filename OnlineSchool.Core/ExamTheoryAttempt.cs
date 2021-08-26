using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Core
{
    public class ExamTheoryAttempt : Entity
    {
        public int QuestionNumber { get; set; }
        public int PageNumber { get; set; }
        public int TheoryQuestionId { get; set; }
        public string Answer { get; set; }
        public decimal Score { get; set; }
        public decimal StudentScore { get; set; }
    }
}

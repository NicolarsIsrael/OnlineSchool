using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Core
{
    public class ExamMcqAttempt : Entity
    {
        public int QuestionNumber { get; set; }
        public int PageNumber { get; set; }
        public int McqId { get; set; }
        public int CorrectAnswerId { get; set; }
        public int SelectedOptionId { get; set; }
        public decimal Score { get; set; }
        public IEnumerable<McqOption> McqOptions { get; set; }
    }
}

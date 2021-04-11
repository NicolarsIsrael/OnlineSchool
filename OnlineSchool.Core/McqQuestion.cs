using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Core
{
    public class McqQuestion : Entity
    {
        public string Question { get; set; }
        public string QuestionFile { get; set; }
        public IEnumerable<McqOption> Options { get; set; }
        public int AnswerId { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
    }
}

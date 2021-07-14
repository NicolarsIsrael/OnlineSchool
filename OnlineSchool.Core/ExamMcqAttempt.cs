using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Core
{
    public class ExamMcqAttempt : Entity
    {
        public int McqId { get; set; }
        public int SelectedOptionId { get; set; }
        public IEnumerable<McqOption> McqOptions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Core
{
    public class McqOption : Entity
    {
        public int AnsId { get; set; }
        public string Option { get; set; }
        public string OptionFile { get; set; }
        public int McqQuestionId { get; set; }
        public McqQuestion Question { get; set; }
    }
}

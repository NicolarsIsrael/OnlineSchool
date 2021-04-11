using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Core
{
    public class McqOption : Entity
    {
        public string Option { get; set; }
        public string OptionFile { get; set; }
        public McqQuestion Question { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSchool.Models
{
    public class ExamAttemptModel
    {
        public int StudentId { get; set; }
        public string StudentMatricNumber { get; set; }
        public string StudentFullname { get; set; }
        public decimal StudentScore { get; set; }
        public decimal TotalGrade { get; set; }
    }
}

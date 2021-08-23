using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSchool.Models
{

    public class ResultsModel
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public int ExamId { get; set; }
        public string ExamTitle { get; set; }
        public decimal PossibleScore { get; set; }
        public int NumberOfAttempts { get; set; }
        public IEnumerable<ExamAttemptModel> Attempts { get; set; }
        public ResultsModel(Exam exam, IEnumerable<ExamAttemptModel> attempts)
        {
            Attempts = attempts;
            CourseTitle = exam.Course.CourseTitle;
            CourseId = exam.Course.Id;
            CourseCode = exam.Course.CourseCode;
            ExamId = exam.Id;
            ExamTitle = exam.ExamTitle;
            PossibleScore = exam.TotalScore;
            NumberOfAttempts = attempts.Count();
        }
    }
    public class ExamAttemptModel
    {
        public int StudentId { get; set; }
        public string StudentMatricNumber { get; set; }
        public string StudentFullname { get; set; }
        public decimal StudentScore { get; set; }
        public decimal TotalGrade { get; set; }
    }

    public class AttemptCompletedModel
    {
        public int ExamId { get; set; }
        public int AttemptId { get; set; }
        public string ExamTitle { get; set; }
        public decimal Score { get; set; }
        public decimal PossibleScore { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public int CourseId { get; set; }
        public AttemptCompletedModel(Exam exam, ExamAttempt attempt)
        {
            ExamId = exam.Id;
            AttemptId = attempt.Id;
            ExamTitle = exam.ExamTitle;
            Score = attempt.Score;
            PossibleScore = attempt.MaximumScore;
            CourseCode = exam.Course.CourseCode;
            CourseTitle = exam.Course.CourseTitle;
            CourseId = exam.Course.Id;
        }
    }
}

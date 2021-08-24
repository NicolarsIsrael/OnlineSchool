using Microsoft.AspNetCore.Http;
using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSchool.Models
{
    public class TheoryQuestionModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string QuestionFile { get; set; }
        public int ExamId { get; set; }
        public decimal Score { get; set; }
        public TheoryQuestionModel(TheoryQuestion theoryQuestion)
        {
            Id = theoryQuestion.Id;
            Question = theoryQuestion.Question;
            QuestionFile = theoryQuestion.QuestionFile;
            ExamId = theoryQuestion.ExamId;
            Score = theoryQuestion.Score;
        }

    }

    public class AddTheoryQuestionModel
    {
        public int ExamId { get; set; }
        [Required(ErrorMessage = "Question is required")]
        [RegularExpression("^[a-zA-Z0-9].*$", ErrorMessage = "Question should begin with letter or number")]
        public string Question { get; set; }
        public IFormFile QuestionFile { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Score should be greater than 1")]
        public decimal Score { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string ExamTitle { get; set; }
        public AddTheoryQuestionModel()
        {

        }
        public AddTheoryQuestionModel(Exam exam)
        {
            ExamId = exam.Id;
            Score = 1;
            CourseId = exam.Course.Id;
            CourseCode = exam.Course.CourseCode;
            ExamTitle = exam.ExamTitle;
        }

        public TheoryQuestion Add(Exam exam)
        {
            return new TheoryQuestion
            {
                Question = Question,
                Score = Score,
                Exam = exam,
            };
        }
    }

    public class EditTheoryQuestionModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Question is required")]
        [RegularExpression("^[a-zA-Z0-9].*$", ErrorMessage = "Question should begin with letter or number")]
        public string Question { get; set; }
        public IFormFile QuestionFile { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Score should be greater than 1")]
        public decimal Score { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string ExamTitle { get; set; }
        public int ExamId { get; set; }

        public EditTheoryQuestionModel()
        {

        }

        public EditTheoryQuestionModel(TheoryQuestion question)
        {
            Id = question.Id;
            Score = question.Score;
            Question = question.Question;
            CourseId = question.Exam.Course.Id;
            CourseCode = question.Exam.Course.CourseCode;
            ExamTitle = question.Exam.ExamTitle;
            ExamId = question.Exam.Id;
        }

        public TheoryQuestion Edit(TheoryQuestion question)
        {
            question.Question = Question;
            question.Score = Score;

            return question;
        }
    }
}

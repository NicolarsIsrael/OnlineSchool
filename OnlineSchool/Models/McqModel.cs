using Microsoft.AspNetCore.Http;
using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSchool.Models
{
    public class AddMcqModel
    {
        public int ExamId { get; set; }
        [Required(ErrorMessage ="Question is required")]
        [RegularExpression("^[a-zA-Z0-9].*$", ErrorMessage = "Question should begin with letter or number")]
        public string Question { get; set; }
        public IFormFile QuestionFile { get; set; }
        public List<AddMcqOptionModel> Options { get; set; }
        public int AnswerId { get; set; }
        public decimal Score { get; set; }
        public AddMcqModel()
        {

        }
        public AddMcqModel(Exam exam)
        {
            ExamId = exam.Id;
            Score = 1;
        }

        public McqQuestion Add(IEnumerable<McqOption> options, Exam exam)
        {
            return new McqQuestion
            {
                Question = Question,
                Options = options,
                Score = Score,
                Exam = exam,
                AnswerId = AnswerId,
            };
        }
    }

    public class ViewMcqQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public decimal Score { get; set; }
        public IEnumerable<ViewMcqOptionModel> Options { get; set; }
        public int AnswerId { get; set; }
        public ViewMcqQuestion(McqQuestion question,bool sendAnswer=false)
        {
            Id = question.Id;
            Question = question.Question;
            Score = question.Score;
            Options = question.Options.Select(op => new ViewMcqOptionModel(op));
            AnswerId = sendAnswer ? question.AnswerId : -2;
        }
    }

    public class AddMcqOptionModel
    {
        public int AnsId { get; set; }
        public string Option { get; set; }
        public IFormFile OptionFile { get; set; }

        public McqOption Add()
        {
            return new McqOption
            {
                Option = Option,
                AnsId = AnsId,
            };
        }
    }

    public class ViewMcqOptionModel
    {
        public int AnsId { get; set; }
        public string Option { get; set; }
        public ViewMcqOptionModel(McqOption option)
        {
            Option = option.Option;
            AnsId = option.AnsId;
        }
    }

    public class EditMcqModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Question is required")]
        [RegularExpression("^[a-zA-Z0-9].*$", ErrorMessage = "Question should begin with letter or number")]
        public string Question { get; set; }
        public IFormFile QuestionFile { get; set; }
        public List<EditMcqOptionModel> Options { get; set; }
        public int AnswerId { get; set; }
        public decimal Score { get; set; }

        public EditMcqModel()
        {

        }

        public EditMcqModel(McqQuestion question)
        {
            Id = question.Id;
            Score = question.Score;
            AnswerId = question.AnswerId;
            Question = question.Question;
            Options = question.Options.Select(op => new EditMcqOptionModel(op)).ToList();
        }

        public McqQuestion Edit(McqQuestion question)
        {
            question.Question = Question;
            question.Score = Score;
            question.AnswerId = AnswerId;

            return question;
        }
    }

    public class EditMcqOptionModel
    {
        public int Id { get; set; }
        public string Option { get; set; }
        public IFormFile OptionFile { get; set; }
        public EditMcqOptionModel()
        {

        }
        public EditMcqOptionModel(McqOption option)
        {
            Id = option.Id;
            Option = option.Option;
        }
        public McqOption Edit(McqOption option)
        {
            option.Option = Option;
            return option;
        }
    }
}

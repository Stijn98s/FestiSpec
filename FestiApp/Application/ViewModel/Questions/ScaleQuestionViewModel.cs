using System.Threading.Tasks;
using FestiApp.persistence;
using FestiDB.Domain;
using System.Collections.Generic;
using System.Linq;

namespace FestiApp.ViewModel.Questions
{
    public class ScaleQuestionViewModel : QuestionViewModel
    {
        private readonly ScaleQuestion _question;

        private readonly IQuestionRepository _questionRepository;



        public int Max { get; set; }


        public ScaleQuestionViewModel(ScaleQuestion question, IQuestionRepository questionRepository) : base(question)
        {
            _question = question;
            _questionRepository = questionRepository;
        }

        public override bool IsValid()
        {
            return (Max <= 10 && Max > 1 && !string.IsNullOrEmpty(Description));
        }

        public override async Task Save()
        {
            _question.Order = Order;
            _question.Min = 1;
            _question.Max = Max;
            _question.Description = Description;
            _question.QuestionnaireId = _question.Questionnaire.Id;
            _question.Questionnaire = null;
            await _questionRepository.InsertAsync(_question);
        }

        public override void Delete()
        {
            if (!string.IsNullOrEmpty(Id))
                _questionRepository.Delete<ScaleQuestion>(Id);
        }
    }
}

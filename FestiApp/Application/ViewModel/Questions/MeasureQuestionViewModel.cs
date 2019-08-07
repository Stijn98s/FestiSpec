using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FestiApp.persistence;
using FestiDB.Domain;

namespace FestiApp.ViewModel.Questions
{
    public class MeasureQuestionViewModel : QuestionViewModel
    {
        private readonly IQuestionRepository _questionRepository;

        private readonly MeasureQuestion _question;

        public string SelectedUnit { get; set; }


        public MeasureQuestionViewModel(MeasureQuestion question, IQuestionRepository questionRepository) : base(question)
        {
            _question = question;
            SelectedUnit = question.Unit;
            _questionRepository = questionRepository;
        }
     

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(Description)) return false;
            if (string.IsNullOrEmpty(SelectedUnit)) return false;
            return true;
        }

        public override async Task Save()
        {
            _question.Order = Order;
            _question.Description = Description;
            _question.QuestionnaireId = _question.Questionnaire.Id;
            _question.Unit = SelectedUnit;
            _question.Questionnaire = null;
            await _questionRepository.InsertAsync(_question);
        }

        public override void Delete()
        {
            if(!string.IsNullOrEmpty(Id))
            _questionRepository.Delete<MeasureQuestion>(Id);
        }
    }
}

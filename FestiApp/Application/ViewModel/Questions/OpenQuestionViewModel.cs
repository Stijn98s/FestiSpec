using System.Threading.Tasks;
using FestiApp.persistence;
using FestiDB.Domain;

namespace FestiApp.ViewModel.Questions
{
    public class OpenQuestionViewModel : QuestionViewModel
    {
        private readonly OpenQuestion _question;
        private readonly IQuestionRepository _questionRepository;

        public OpenQuestionViewModel(OpenQuestion question, IQuestionRepository questionRepository) : base(question)
        {
            _question = question;
            _questionRepository = questionRepository;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(Description);
        }

        public override async Task Save()
        {
            _question.Order = Order;
            _question.Description = Description;
            _question.QuestionnaireId = _question.Questionnaire.Id;
            _question.Questionnaire = null;
            await _questionRepository.InsertAsync(_question);
        }

        public override void Delete()
        {
            if (!string.IsNullOrEmpty(Id))
                _questionRepository.Delete<OpenQuestion>(Id);
        }
    }
}

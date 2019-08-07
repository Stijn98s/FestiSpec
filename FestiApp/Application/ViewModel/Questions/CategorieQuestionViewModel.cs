using FestiApp.persistence;
using FestiApp.Util;
using FestiDB.Domain;
using System.Threading.Tasks;

namespace FestiApp.ViewModel.Questions
{
    public class CategorieQuestionViewModel : QuestionViewModel
    {
        private readonly CategorieQuestion _question;
        private readonly IQuestionRepository _questionRepository;

        public CategorieQuestionViewModel(CategorieQuestion question, IQuestionRepository questionRepository) : base(question)
        {
            _question = question;
            _questionRepository = questionRepository;
        }

        public override bool IsValid()
        {
            if (!ValidationHelper.IsNotEmpty(Description)) return false;
            if (!ValidationHelper.IsBetweenLength(250, 1, Description)) return false;
            return true;
        }

        public override async Task Save()
        {
            _question.Description = Description;
            _question.QuestionnaireId = _question.Questionnaire.Id;
            _question.Questionnaire = null;
            await _questionRepository.InsertAsync(_question);
        }

        public override void Delete()
        {
            if (!string.IsNullOrEmpty(Id))
                _questionRepository.Delete<CategorieQuestion>(Id);
        }
    }
}

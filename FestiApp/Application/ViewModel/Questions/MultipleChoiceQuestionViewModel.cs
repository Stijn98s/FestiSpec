using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using FestiApp.persistence;
using FestiApp.Util;
using FestiDB.Domain;
using GalaSoft.MvvmLight.CommandWpf;

namespace FestiApp.ViewModel.Questions
{
    public class MultipleChoiceQuestionViewModel : QuestionViewModel
    {

        private readonly IQuestionRepository _questionRepository;

        private MultipleChoiceQuestion _question;
   

        public MultipleChoiceQuestionViewModel(MultipleChoiceQuestion question, IQuestionRepository questionRepository) : base(question)
        {
            _questionRepository = questionRepository;
            _question = question;
            Options = new ObservableCollection<MultipleChoiceQuestionOption>();
            AddOption();
            AddOptionCommand = new RelayCommand(AddOption, CanAddRow);
        }

        public RelayCommand AddOptionCommand { get; set; }

        private void AddOption()
        {
            Options.Add(new MultipleChoiceQuestionOption(){Id = Guid.NewGuid().ToString()});
        }

        public ICollection<MultipleChoiceQuestionOption> Options { get; set; }

        private bool CanAddRow()
        { 
            if (!ValidationHelper.IsNotEmpty(Description)) return false;
            if (!ValidationHelper.IsNotEmpty(Options.LastOrDefault()?.Value)) return false;
            if (Options.Count >= 4) return false;
            return true;
        }

        public override bool IsValid()
        {
            if(Options.Count < 2) return false;
            if (string.IsNullOrEmpty(Description)) return false;
            if (Options.Any(el => string.IsNullOrEmpty(el.Value))) return false;
            return true;
        }

        public override async Task Save()
        {
            _question.Order = Order;
            _question.Options = Options.ToList();
            _question.Description = Description;
            _question.QuestionnaireId = _question.Questionnaire.Id;
            _question.Questionnaire = null;
            await _questionRepository.InsertAsync(_question);
        }

        public override void Delete()
        {
            if (!string.IsNullOrEmpty(Id))
                _questionRepository.Delete<MultipleChoiceQuestion>(Id);
        }
    }
}

using FestiApp.ViewModel.Questions;
using FestiDB.Domain;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;

namespace FestiApp.ViewModel.Questionnaires
{
    public class QuestionnaireViewModel : ViewModelBase
    {
        internal Questionnaire Elem { get; }

        public QuestionnaireViewModel()
        {
        }

        public QuestionnaireViewModel(Questionnaire elem)
        {
            this.Elem = elem;
        }

        public string Id { get; set; }

        public string Name { get; set; } = "";
        public string Description { get; set; } = "";

        public string Theme { get; set; } = "";

        public DateTime From { get; set; } = DateTime.Now;

        public DateTime To { get; set; } = DateTime.Now;

        public ObservableCollection<QuestionViewModel> QuestionViewModels { get; set; } = new ObservableCollection<QuestionViewModel>();

        public ObservableCollection<Inspector> AssignedTo { get; set; } = new ObservableCollection<Inspector>();
    }
}

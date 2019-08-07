using FestiApp.persistence;
using FestiApp.Util;
using FestiApp.View;
using FestiApp.View.Questionaire;
using FestiApp.ViewModel.Questions;
using FestiDB.Domain;
using FestiDB.Persistence;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;

namespace FestiApp.ViewModel.Questionnaires
{
    public class EditQuestionnaireViewModel : ViewModelBase, IEditViewModel<QuestionnaireViewModel>
    {
        private readonly IQuestionRepository _client;
        private readonly QuestionFactory _questionFactory;
        public IEnumerable<string> QuestionTypes => _questionFactory.QuestionTypes;

        public string SelectedType { get; set; }

        public ICommand AddQuestionCommand { get; set; }
        public ICommand ShowDescriptionCommand { get; set; }

        private readonly IEditViewModel<QuestionnaireViewModel> _editVm;


        public EditQuestionnaireViewModel(QuestionFactory questionFactory, IQuestionRepository client, IEditViewModel<QuestionnaireViewModel> editVm)
        {
            _client = client;
            _editVm = editVm;
            _questionFactory = questionFactory;
            _editVm.AddPredicate(CanExecute);
            SelectedType = QuestionTypes.First();
            Deleted = new List<QuestionViewModel>();
            DeleteSelectedCommand = new RelayCommand<QuestionViewModel>(DeleteVM);
            AddQuestionCommand = new RelayCommand(AddQuestion);
            ShowDescriptionCommand = new RelayCommand(ShowDescription);
            AddEntityCommand = new RelayCommand<IClosable>(SaveEntities, (el) => CanExecute());
            QuestionViewModels = new ObservableCollection<QuestionViewModel>();
            editVm.EntityViewModel.QuestionViewModels = new ObservableCollection<QuestionViewModel>();
            GetQuestions();
        }

        private void ShowDescription()
        {
            var window = new QuestionnaireEditDescriptionPage();
            window.ShowDialog();
        }

        private async void SaveEntities(IClosable window)
        {
            _editVm.IsLoading = true;
            RaisePropertyChanged("IsLoading");
            foreach (var questionViewModel in EntityViewModel.QuestionViewModels)
            {
                await questionViewModel.Save();
            }

            foreach (var questionViewModel in Deleted)
            {
                questionViewModel.Delete();
            }

            if (_editVm.Entity is Questionnaire questionnaire)
            {
                questionnaire.Questions = null;
            }
            _editVm.IsLoading = false;
            _editVm.UpdateEntityCommand.Execute(window);
        }

        private void DeleteVM(QuestionViewModel obj)
        {
            var indexOf = QuestionViewModels.IndexOf(obj);

            QuestionViewModels.Remove(obj);
            EntityViewModel.QuestionViewModels.Remove(obj);
            for (int i = indexOf; i < QuestionViewModels.Count; i++)
            {
                QuestionViewModels[i].Order = i +1;
            }
            Deleted.Add(obj);
        }

        private ICollection<QuestionViewModel> Deleted { get; set; }

        private bool CanExecute()
        {
            if (EntityViewModel.QuestionViewModels.Any(el => !el.IsValid())) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.Name)) return false;
            if (!ValidationHelper.IsBetweenLength(45, 2, EntityViewModel.Name)) return false;
            if (!ValidationHelper.IsValidTitle(EntityViewModel.Name)) return false;

            if (!ValidationHelper.IsBetweenLength(20, 0, EntityViewModel.Theme)) return false;
            if (!ValidationHelper.IsBetweenLength(10000, 0, EntityViewModel.Description)) return false;
            return true;
        }

        private async void GetQuestions()
        {
            var items = await _client.GetAll(EntityViewModel.Id);
            QuestionViewModels.CopyFrom(items.OrderBy(el => el.Order).ToList());
        }

        public ObservableCollection<QuestionViewModel> QuestionViewModels { get; set; }

        public IEntity Entity => _editVm.Entity;

        public RelayCommand DeleteEntityCommand
        {
            get => _editVm.DeleteEntityCommand;
            set => _editVm.DeleteEntityCommand = value;
        }

        public QuestionnaireViewModel EntityViewModel => _editVm.EntityViewModel;

        public RelayCommand<IClosable> UpdateEntityCommand
        {
            get => _editVm.UpdateEntityCommand;
            set => _editVm.UpdateEntityCommand = value;
        }

        public bool IsLoading
        {
            get => _editVm.IsLoading;
            set => _editVm.IsLoading = value;
        }

        public bool IsNotLoading => _editVm.IsNotLoading;

        public RelayCommand<IClosable> CloseWindowCommand
        {
            get => _editVm.CloseWindowCommand;
            set => _editVm.CloseWindowCommand = value;
        }

        public RelayCommand<IClosable> AddEntityCommand { get; set; }

        public RelayCommand<QuestionViewModel> DeleteSelectedCommand { get; set; }

        private void AddQuestion()
        {
            var count = QuestionViewModels.Count;
            var elem = _questionFactory.Create((Questionnaire)_editVm.Entity, SelectedType, count +1);
            EntityViewModel.QuestionViewModels.Add(elem);
            QuestionViewModels.Add(elem);
        }

        public void AddPredicate(Func<bool> canExecute)
        {
            _editVm.AddPredicate(canExecute);
        }
    }
}

using AutoMapper;
using FestiApp.persistence;
using FestiApp.Util;
using FestiApp.View;
using FestiApp.ViewModel.Events;
using FestiApp.ViewModel.Questions;
using FestiDB.Domain;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using FestiApp.View.Questionaire;

namespace FestiApp.ViewModel.Questionnaires
{
    public class AddQuestionnaireViewModel : GenericAddEntityViewModel<QuestionnaireViewModel, Questionnaire>
    {
        private readonly IQuestionFactory _questionFactory;

        public IEnumerable<string> QuestionTypes => _questionFactory.QuestionTypes;

        public string SelectedType { get; set; }
        private readonly Questionnaire _questionnaire;

        public ICommand AddQuestionCommand { get; set; }
        public ICommand ShowDescriptionCommand { get; set; }
        
        public AddQuestionnaireViewModel(IQuestionFactory questionFactory, IAddableList<Questionnaire> list,
            IMapper mapper, IEditViewModel<EventViewModel> editVm, IFestiClient client) : base(list, mapper, client)
        {
            DeleteSelectedCommand = new RelayCommand<QuestionViewModel>(DeleteSelected);
            _questionFactory = questionFactory;
            SelectedType = QuestionTypes.First();
            AddQuestionCommand = new RelayCommand(AddQuestionAsync);
            ShowDescriptionCommand = new RelayCommand(ShowDescription);

            _questionnaire = new Questionnaire()
            {
                EventId = editVm.Entity.Id
            };
        }

        private void ShowDescription()
        {
            var window = new QuestionnaireAddDescriptionPage();
            window.ShowDialog();
        }


        public RelayCommand<QuestionViewModel> DeleteSelectedCommand { get; set; }

        private void DeleteSelected(QuestionViewModel qvm)
        {
            var indexOf = EntityViewModel.QuestionViewModels.IndexOf(qvm);

            EntityViewModel.QuestionViewModels.Remove(qvm);
            for (int i = indexOf; i < EntityViewModel.QuestionViewModels.Count; i++)
            {
                EntityViewModel.QuestionViewModels[i].Order = i +1;
            }
        }

        public void AddQuestionAsync()
        {
            EntityViewModel.QuestionViewModels.Add(_questionFactory.Create(_questionnaire, SelectedType, EntityViewModel.QuestionViewModels.Count +1));
        }

        protected override bool CanAddEntity(IClosable window)
        {
            if (EntityViewModel.QuestionViewModels.Count < 1) return false;
            if (EntityViewModel.QuestionViewModels.Any(el => !el.IsValid())) return false;
            if (base.IsLoading) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.Name)) return false;
            if (!ValidationHelper.IsBetweenLength(45, 2, EntityViewModel.Name)) return false;
            if (!ValidationHelper.IsValidTitle(EntityViewModel.Name)) return false;
            
            if (!ValidationHelper.IsBetweenLength(20, 0, EntityViewModel.Theme)) return false;
            if (!ValidationHelper.IsBetweenLength(10000, 0, EntityViewModel.Description)) return false;

            return true;
        }


        public override async void AddEntity(IClosable window)
        {
            try
            {
                base.IsLoading = true;
                AddEntityCommand.RaiseCanExecuteChanged();
                _mapper.Map(EntityViewModel, _questionnaire);
                await _client.GetSyncTable<Questionnaire>().InsertAsync(_questionnaire);
                await _client.SyncAsync();

                foreach (var questionViewModel in EntityViewModel.QuestionViewModels)
                {
                    await questionViewModel.Save();
                }

                _list.AddEntity(_questionnaire);
                window?.Close();
                await _client.SyncAsync();
            }
            finally
            {
                base.IsLoading = false;
            }
        }
    }
}
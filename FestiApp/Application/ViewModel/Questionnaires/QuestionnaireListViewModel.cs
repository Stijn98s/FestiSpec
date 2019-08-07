using AutoMapper;
using FestiApp.persistence;
using FestiApp.Util;
using FestiApp.View.Event;
using FestiApp.View.Questionnaire;
using FestiApp.ViewModel.Events;
using FestiDB.Domain;
using GalaSoft.MvvmLight.CommandWpf;
using Ninject;
using System.Linq;
using System.Windows.Input;

namespace FestiApp.ViewModel.Questionnaires
{
    public class QuestionnaireListViewModel : ListViewModelBase<QuestionnaireViewModel, Questionnaire>
    {
        private readonly IQuestionnaireRepository _repo;
        private readonly IEditViewModel<EventViewModel> _eventVm;
        public INetStatusService NetService { get; }

        public QuestionnaireListViewModel(IQuestionnaireRepository repo, FestiMSClient client, IMapper mapper, [Named("ListEv")] IEditViewModel<EventViewModel> eventVm, INetStatusService netService)
            : base(client, mapper)
        {
            _repo = repo;
            _eventVm = eventVm;
            OpenPlanCommand = new RelayCommand(OpenPlan, CanPlanInspectorIn);
            // ReSharper disable once VirtualMemberCallInConstructor
            Refresh();
            NetService = netService;
        }

        public void OpenPlan()
        {
            var window = new ScheduleEventPage();
            window.ShowDialog();
        }

        public bool CanPlanInspectorIn()
        {
            return NetService.IsActive;
        }

        protected override void ShowAddEntity()
        {
            var window = new AddQuestionnairePage();
            window.ShowDialog();
        }

        protected override async void Refresh()
        {
            if (_client != null && _eventVm != null)
            {
                var questionnaires = await _repo.GetAll(_eventVm.EntityViewModel?.Id);
                ViewModels
                    .CopyFrom(
                        questionnaires
                            .Select(elem =>
                                new GenericEditEntityViewModel<QuestionnaireViewModel, Questionnaire>(_client, _mapper,
                                    elem))
                            .ToList()
                    );
            }
        }

        protected override void EditSelectedViewModel()
        {
            var window = new EditQuestionnairePage();
            window.ShowDialog();
        }

        public ICommand OpenPlanCommand { get; set; }
    }
}
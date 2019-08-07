using AutoMapper;
using FestiApp.persistence;
using FestiApp.View;
using FestiApp.View.Advice;
using FestiApp.View.Contacts;
using FestiApp.View.Event;
using FestiApp.ViewModel.Customers;
using FestiApp.ViewModel.Questionnaires;
using FestiDB.Persistence;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using FestiApp.Util;

namespace FestiApp.ViewModel.Events
{
    public class EventDashboardViewModel : ViewModelBase, IEditViewModel<EventViewModel>
    {
        private readonly IEditViewModel<EventViewModel> _editVm;

        public INetStatusService NetService { get; set; }

        public RelayCommand ShowAdviceCommand { get; set; }

        public ICommand ShowContactsCommand { get; set; }

        public ICommand ShowDescriptionCommand { get; set; }

        public EventDashboardViewModel(IEditViewModel<EventViewModel> editVm, FestiMSClient client, QuestionnaireListViewModel question, IMapper mapper, INetStatusService statusService)
        {
            _editVm = editVm;
            Questions = question;
            NetService = statusService;
            _editVm.AddPredicate(CanExecute);

            Task.Run(async () =>
            {
                var customer = await client.Customers.GetCustomer(editVm.EntityViewModel.CustomerId);
                CustomerVM = mapper.Map<CustomerViewModel>(customer);
                RaisePropertyChanged("CustomerVM");
            });

            ShowContactsCommand = new RelayCommand(ShowContacts);
            ShowAdviceCommand = new RelayCommand(ShowAdviceBuilder, CanShowAdviceBuilder);
            ShowDescriptionCommand = new RelayCommand(ShowDescription);
            NetService.SubscribeWithAction(ShowAdviceCommand.RaiseCanExecuteChanged);
        }

        public CustomerViewModel CustomerVM { get; set; }

        private void ShowContacts()
        {
            var window = new ShowContactsWindow();
            window.ShowDialog();
        }

        private void ShowAdviceBuilder()
        {
            var window = new BuilderAdvicePage();
            window.ShowDialog();
        }

        private void ShowDescription()
        {
            var window = new EventDescriptionPage();
            window.ShowDialog();
        }

        private bool CanShowAdviceBuilder()
        {
            return NetService.IsActive;
        }

        public IEntity Entity => _editVm.Entity;

        public RelayCommand DeleteEntityCommand { get; set; }

        public EventViewModel EntityViewModel => _editVm.EntityViewModel;

        private bool CanExecute()
        {
            if (!ValidationHelper.IsBetweenLength(1000, 0, EntityViewModel.Description)) return false;
            return true;
        }

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
            get => _editVm.CloseWindowCommand; set => _editVm.CloseWindowCommand = value;
        }

        public QuestionnaireListViewModel Questions { get; }

        public void AddPredicate(Func<bool> canExecute)
        {
            _editVm.AddPredicate(canExecute);
        }
    }
}
using AutoMapper;
using FestiApp.persistence;
using FestiApp.Util;
using FestiApp.View;
using FestiApp.View.Questionnaire;
using FestiApp.ViewModel.Validation;
using FestiDB.Domain;
using FestiDB.Persistence;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FestiApp.ViewModel.Events
{
    public class EditEventViewModel : ViewModelBase, IEditViewModel<EventViewModel>
    {
        private readonly IEditViewModel<EventViewModel> _selectedViewModel;
        private Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                EntityViewModel.CustomerId = value.Id;
                _selectedCustomer = value;
                RaisePropertyChanged();
            }
        }

        private bool CanExecute()
        {
            IsSubsequentDateValidationRule.DateTime = EntityViewModel.StartDate;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.Name)) return false;
            if (!ValidationHelper.IsBetweenLength(45, 2, EntityViewModel.Name)) return false;

            if (!ValidationHelper.IsEmpty(EntityViewModel.StartDate)) return false;

            if (!ValidationHelper.IsEmpty(EntityViewModel.EndDate)) return false;
            if (!ValidationHelper.IsSubsequentDate(EntityViewModel.StartDate, EntityViewModel.EndDate)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.Location)) return false;
            if (!ValidationHelper.IsBetweenLength(50, 2, EntityViewModel.Location)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.PostalCode)) return false;
            if (!ValidationHelper.IsPostalCode(EntityViewModel.PostalCode)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.HouseNumber)) return false;
            if (!ValidationHelper.IsHouseNumber(EntityViewModel.HouseNumber)) return false;
            if (_selectedCustomer == null) return false;

            return true;
        }

        public EditEventViewModel(IEditViewModel<EventViewModel> selectedViewModel, ICustomerRepository customerRepository, IMapper mapper)
        {
            _selectedViewModel = selectedViewModel;
            selectedViewModel.AddPredicate(CanExecute);
            ShowCreateQuestionnaireCommand = new RelayCommand(AddQuestionaire);

            Task.Run(async () =>
            {
                Customers = await customerRepository.GetCustomers();
                SelectedCustomer = Customers.FirstOrDefault(el => el.Id == EntityViewModel.CustomerId);
                RaisePropertyChanged(() => Customers);
            });

            IsSubsequentDateValidationRule.DateTime = EntityViewModel.StartDate;
        }

        private void AddQuestionaire()
        {
            var window = new AddQuestionnairePage();
            window.Show();
        }

        public IEntity Entity => _selectedViewModel.Entity;

        public RelayCommand DeleteEntityCommand
        {
            get => _selectedViewModel.DeleteEntityCommand;
            set => _selectedViewModel.DeleteEntityCommand = value;
        }

        public EventViewModel EntityViewModel => _selectedViewModel.EntityViewModel;

        public RelayCommand<IClosable> UpdateEntityCommand
        {
            get => _selectedViewModel.UpdateEntityCommand;
            set => _selectedViewModel.UpdateEntityCommand = value;
        }

        public bool IsLoading
        {
            get => _selectedViewModel.IsLoading;
            set => _selectedViewModel.IsLoading = value;
        }

        public void AddPredicate(Func<bool> canExecute)
        {
            _selectedViewModel.AddPredicate(canExecute);
        }

        public bool IsNotLoading => _selectedViewModel.IsNotLoading;

        public RelayCommand<IClosable> CloseWindowCommand
        {
            get => _selectedViewModel.CloseWindowCommand;
            set => _selectedViewModel.CloseWindowCommand = value;
        }

        public ICommand ShowCreateQuestionnaireCommand { get; set; }
        public ICollection<Customer> Customers { get; private set; }
    }
}

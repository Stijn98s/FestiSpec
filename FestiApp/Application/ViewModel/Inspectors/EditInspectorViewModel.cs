using FestiApp.persistence;
using FestiApp.Util;
using FestiApp.View;
using FestiDB.Persistence;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FestiApp.ViewModel.Inspectors
{
    public class EditInspectorViewModel : IEditViewModel<InspectorViewModel>
    {
        private readonly IEditViewModel<InspectorViewModel> _editVm;
        public ICommand CloseCommand { get; set; }
        private ICollection<string> Emails { get; set; }

        public EditInspectorViewModel(IUserRepository repository, IEditViewModel<InspectorViewModel> editVm)
        {
            _editVm = editVm;

            CloseCommand = new RelayCommand<IClosable>(CloseWindow);
            Task.Run(async () =>
            {
                Emails = await repository.GetEmailsAsync(EntityViewModel.Id);
            });
            _editVm.AddPredicate(CanExecute);
        }

        private bool CanExecute()
        {
            if (!ValidationHelper.IsNotEmpty(EntityViewModel.FirstName)) return false;
            if (!ValidationHelper.IsCharacterOnly(EntityViewModel.FirstName)) return false;
            if (!ValidationHelper.IsBetweenLength(45, 2, EntityViewModel.FirstName)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.LastName)) return false;
            if (!ValidationHelper.IsCharacterOnly(EntityViewModel.LastName)) return false;
            if (!ValidationHelper.IsBetweenLength(45, 2, EntityViewModel.LastName)) return false;

            if (!ValidationHelper.IsEmpty(EntityViewModel.BirthDate)) return false;
            if (!ValidationHelper.IsPastDate(EntityViewModel.BirthDate)) return false;

            if (!ValidationHelper.IsEmpty(EntityViewModel.Gender)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.HouseNumber)) return false;
            if (!ValidationHelper.IsHouseNumber(EntityViewModel.HouseNumber)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.Email)) return false;
            if (!ValidationHelper.IsEmail(EntityViewModel.Email)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.PostalCode)) return false;
            if (!ValidationHelper.IsPostalCode(EntityViewModel.PostalCode)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.Phone)) return false;
            if (!ValidationHelper.IsPhoneNumber(EntityViewModel.Phone)) return false;

            if (Emails.Any(elem => elem == EntityViewModel.Email)) return false;

            return true;
        }

        private void CloseWindow(IClosable window)
        {
            window?.Close();
        }

        public IEntity Entity => _editVm.Entity;

        public RelayCommand DeleteEntityCommand
        {
            get => _editVm.DeleteEntityCommand;
            set => _editVm.DeleteEntityCommand = value;
        }

        public InspectorViewModel EntityViewModel => _editVm.EntityViewModel;

        public RelayCommand<IClosable> UpdateEntityCommand
        {
            get => _editVm.UpdateEntityCommand;
            set => _editVm.UpdateEntityCommand = value;
        }

        public RelayCommand<IClosable> CloseWindowCommand
        {
            get => _editVm.CloseWindowCommand;
            set => _editVm.CloseWindowCommand = value;
        }

        public bool IsLoading { get => _editVm.IsLoading; set => _editVm.IsLoading = value; }

        public bool IsNotLoading => _editVm.IsNotLoading;

        public void AddPredicate(Func<bool> canExecute)
        {
            _editVm.AddPredicate(canExecute);
        }
    }
}

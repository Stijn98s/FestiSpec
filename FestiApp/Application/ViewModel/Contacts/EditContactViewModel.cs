using FestiApp.Util;
using FestiApp.View;
using FestiDB.Persistence;
using GalaSoft.MvvmLight.CommandWpf;
using Ninject;
using System;

namespace FestiApp.ViewModel.Contacts
{
    public class EditContactViewModel : IEditViewModel<ContactViewModel>
    {
        private readonly IEditViewModel<ContactViewModel> _editVm;

        public EditContactViewModel(IEditViewModel<ContactViewModel> editVm)
        {
            _editVm = editVm;
            _editVm.AddPredicate(CanExecute);
        }

        private bool CanExecute()
        {
            if (!ValidationHelper.IsNotEmpty(EntityViewModel.FirstName)) return false;
            if (!ValidationHelper.IsBetweenLength(45, 2, EntityViewModel.FirstName)) return false;
            if (!ValidationHelper.IsCharacterOnly(EntityViewModel.FirstName)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.LastName)) return false;
            if (!ValidationHelper.IsBetweenLength(45, 2, EntityViewModel.LastName)) return false;
            if (!ValidationHelper.IsCharacterOnly(EntityViewModel.LastName)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.Email)) return false;
            if (!ValidationHelper.IsEmail(EntityViewModel.Email)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.PhoneNumber)) return false;
            if (!ValidationHelper.IsPhoneNumber(EntityViewModel.PhoneNumber)) return false;

            if (!ValidationHelper.IsBetweenLength(50, 0, EntityViewModel.Role)) return false;

            if (!ValidationHelper.IsBetweenLength(200, 0, EntityViewModel.Note)) return false;
            return true;
        }

        public IEntity Entity => _editVm.Entity;

        public RelayCommand DeleteEntityCommand
        {
            get => _editVm.DeleteEntityCommand;
            set => _editVm.DeleteEntityCommand = value;
        }

        public ContactViewModel EntityViewModel => _editVm.EntityViewModel;

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

        public void AddPredicate(Func<bool> canExecute)
        {
            _editVm.AddPredicate(canExecute);
        }
    }
}

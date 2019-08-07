using FestiApp.Util;
using FestiApp.View;
using FestiDB.Persistence;
using GalaSoft.MvvmLight.CommandWpf;
using Ninject;
using System;

namespace FestiApp.ViewModel.Customers
{
    public class EditCustomerViewModel : IEditViewModel<CustomerViewModel>
    {
        private readonly IEditViewModel<CustomerViewModel> _editVm;

        public EditCustomerViewModel(IEditViewModel<CustomerViewModel> editVm)
        {
            _editVm = editVm;
            _editVm.AddPredicate(CanExecute);
        }

        private bool CanExecute()
        {
            if (!ValidationHelper.IsNotEmpty(EntityViewModel.Name)) return false;
            if (!ValidationHelper.IsBetweenLength(45, 2, EntityViewModel.Name)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.KvK)) return false;
            if (!ValidationHelper.IsKVKNumber(EntityViewModel.KvK)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.PostalCode)) return false;
            if (!ValidationHelper.IsPostalCode(EntityViewModel.PostalCode)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.HouseNumber)) return false;
            if (!ValidationHelper.IsHouseNumber(EntityViewModel.HouseNumber)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.PhoneNumber)) return false;
            if (!ValidationHelper.IsPhoneNumber(EntityViewModel.PhoneNumber)) return false;
            return true;
        }

        public IEntity Entity => _editVm.Entity;

        public RelayCommand DeleteEntityCommand
        {
            get => _editVm.DeleteEntityCommand;
            set => _editVm.DeleteEntityCommand = value;
        }

        public CustomerViewModel EntityViewModel => _editVm.EntityViewModel;

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

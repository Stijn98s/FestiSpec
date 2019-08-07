using FestiApp.persistence;
using FestiApp.Util;
using FestiApp.View;
using FestiApp.ViewModel.Validation;
using FestiDB.Persistence;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows.Input;

namespace FestiApp.ViewModel.Users
{
    public class ChangeUserPasswordViewModel : IEditViewModel<UserViewModel>

    {
        private readonly IEditViewModel<UserViewModel> _editVm;
        public ICommand CloseCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        private readonly FestiMSClient _festiMSClient;
        private readonly INetStatusService _statusService;
        public string Password { get; set; }
        public string SecondPassword { get; set; }

        private bool _isLoading = false;
        public bool IsNotLoading => !IsLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => _isLoading = value;
        }

        public ChangeUserPasswordViewModel(IEditViewModel<UserViewModel> editVm, FestiMSClient festiMSClient, INetStatusService statusService)
        {
            Password = "";
            SecondPassword = "";
            _editVm = editVm;
            _festiMSClient = festiMSClient;
            _statusService = statusService;
            CloseCommand = new RelayCommand<IClosable>(CloseWindow);
            ChangePasswordCommand = new RelayCommand<IClosable>(ChangePassword, CanEditPassword);
            IsSamePasswordValidationRule.Password = SecondPassword;
        }

        public bool CanEditPassword(IClosable window)
        {

            IsSamePasswordValidationRule.Password = SecondPassword;
            if (_isLoading) return false;
            if (!ValidationHelper.IsValidPassword(Password)) return false;
            if (!ValidationHelper.IsSamePassword(Password, SecondPassword)) return false;
            if (!ValidationHelper.IsNotEmpty(Password)) return false;
            return _statusService.IsActive;
        }

        private void CloseWindow(IClosable obj)
        {
            obj?.Close();
        }

        private async void ChangePassword(IClosable window)
        {
            _isLoading = true;
            await _festiMSClient.Users.ChangePassword(_editVm.Entity.Id, Password);
            window?.Close();
            _isLoading = false;
        }

        public IEntity Entity => _editVm.Entity;

        public UserViewModel EntityViewModel => _editVm.EntityViewModel;

        public RelayCommand<IClosable> CloseWindowCommand
        {
            get => _editVm.CloseWindowCommand;
            set => _editVm.CloseWindowCommand = value;
        }

        public void AddPredicate(Func<bool> canExecute)
        {
            _editVm.AddPredicate(canExecute);
        }

        public RelayCommand<IClosable> UpdateEntityCommand { get => _editVm.UpdateEntityCommand; set => _editVm.UpdateEntityCommand = value; }

        public RelayCommand DeleteEntityCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

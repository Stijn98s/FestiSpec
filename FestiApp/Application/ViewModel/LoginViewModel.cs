using FestiApp.persistence;
using FestiApp.Util;
using FestiApp.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FestiApp.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {

        private readonly FestiMSClient _msClient;
        private bool _isLoading = false;
        public IClosable Window { get; set; }

        public LoginViewModel(FestiMSClient msClient)
        {
            _msClient = msClient;
            LoginCommand = new RelayCommand<IClosable>(Login, CanLogin);
            PasswordChangedCommand = new RelayCommand<PasswordBox>(ExecutePasswordChangedCommand);
        }

        public ICommand LoginCommand { get; set; }

        private bool CanLogin(IClosable window)
        {
            if (IsLoading) return false;
            return ValidationHelper.IsNotEmpty(Password) && ValidationHelper.IsNotEmpty(Username);
        }

        private async void Login(IClosable window)
        {
            IsLoading = true;
            try
            {
                await _msClient.LoginAsync(Username, Password);
                var newWindow = new MainWindow();
                newWindow.Show();
                window?.Close();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.Write(e);
                MessageBox.Show(e.Response.ReasonPhrase.Equals("Unauthorized")
                    ? "Het gebruikersnaam of wachtwoord is fout, probeer het opnieuw."
                    : "Er is iets fout gegaan bij het inloggen, probeer het opnieuw.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.Equals("Er is een fout opgetreden bij het verzenden van het aanvraag.")
                    ? e.InnerException.Message
                    : "Er is iets fout gegaan bij het inloggen, probeer het opnieuw.");
            }

            IsLoading = false;

        }

        private void ExecutePasswordChangedCommand(PasswordBox obj)
        {
            if (obj != null)
            {
                Password = obj.Password;
            }
        }

        public ICommand PasswordChangedCommand { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                RaisePropertyChanged();
                RaisePropertyChanged($"IsNotLoading");
            }
        }

        public bool IsNotLoading => !IsLoading;
    }
}
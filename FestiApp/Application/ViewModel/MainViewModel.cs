using FestiApp.persistence;
using FestiApp.View;
using FestiApp.View.Customer;
using FestiApp.View.Event;
using FestiApp.View.Inspector;
using FestiApp.View.User;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.WindowsAzure.MobileServices;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FestiApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Page _view;
        private FestiMSClient _client;
        public INetStatusService NetService { get; }

        public Page View
        {
            get => _view;
            set
            {
                _view = value;
                RaisePropertyChanged();
            }
        }

        private string _windowname = "Festispec";

        public string WindowName
        {
            get => _windowname;
            set
            {
                _windowname = value;
                RaisePropertyChanged();
            }
        }
        public ICommand OpenDashboardCommand { get; set; }
        public ICommand OpenListEventCommand { get; set; }
        public ICommand OpenListInspectorCommand { get; set; }
        public ICommand OpenListEmployeeCommand { get; set; }
        public ICommand OpenListCustomerCommand { get; }
        public ICommand LogoutCommand { get; set; }
        public ICommand ClearStoreCommand { get; set; }
        public ICommand OpenEventDashboardCommand { get; set; }
        public ICommand OpenUserAccountDashBoardCommand { get; set; }


        public MainViewModel(FestiMSClient client, INetStatusService netService)
        {
            View = new DashboardPage();
            LogoutCommand = new RelayCommand<MainWindow>(Logout);
            OpenDashboardCommand = new RelayCommand(OpenDashboard);
            OpenListEventCommand = new RelayCommand(OpenListEvent);
            OpenListInspectorCommand = new RelayCommand(OpenListInspector);
            OpenListEmployeeCommand = new RelayCommand(OpenListEmployee);
            OpenListCustomerCommand = new RelayCommand(OpenListCustomer);
            ClearStoreCommand = new RelayCommand(ClearStore);
            OpenEventDashboardCommand = new RelayCommand(OpenEventDashBoard);
            OpenUserAccountDashBoardCommand = new RelayCommand(OpenUserAccountDashBoardPage);
            _client = client;
            NetService = netService;
        }

        private void OpenUserAccountDashBoardPage()
        {
            View = new UserAccountDashBoardPage();
        }

        private async void Logout(Window window)
        {
            try
            {
                await _client.LogoutAsync();
                var newWindow = new LoginWindow();
                window?.Close();
                View = new DashboardPage();
                newWindow.Show();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.Write(e);
            }
        }

        private void OpenEventDashBoard()
        {
            View = new EventDashboard();
        }

        private void ClearStore()
        {
            _client.ClearStore();
        }

        private void OpenListCustomer()
        {
            View = new ListCustomerPage();
        }

        private void OpenDashboard()
        {
            View = new DashboardPage();
        }

        private void OpenListEvent()
        {
            View = new ListEventPage();
        }

        private void OpenListInspector()
        {
            View = new ListInspectorPage();
        }

        private void OpenListEmployee()
        {
            View = new ListUserPage();
        }

    }
}
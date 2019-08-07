using AutoMapper;
using FestiApp.View.Customer;
using FestiDB.Domain;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using FestiApp.View.Contacts;

namespace FestiApp.ViewModel.Customers
{
    public class CustomerListViewModel : ListViewModelBase<CustomerViewModel, Customer>
    {
        public ICommand ShowContactAdd { get; set; }

        public CustomerListViewModel(IFestiClient client, IMapper mapper) : base(client, mapper)
        {
            ShowContactAdd = new RelayCommand(AddContactViewModel);
        }

        protected override void ShowAddEntity()
        {
            var window = new AddCustomerPage();
            window.ShowDialog();
        }

        protected override void EditSelectedViewModel()
        {
            var window = new EditCustomerPage();
            window.ShowDialog();
        }

        protected void AddContactViewModel()
        {
            var window = new ListContactPage();
            window.ShowDialog();
        }
    }
}

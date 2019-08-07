using GalaSoft.MvvmLight;

namespace FestiApp.ViewModel.Customers
{
    public class CustomerViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public string KvK { get; set; }
        public string PostalCode { get; set; }
        public string HouseNumber { get; set; }
        public string PhoneNumber { get; set; }
    }
}
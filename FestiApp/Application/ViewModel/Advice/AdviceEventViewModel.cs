using FestiApp.persistence;
using FestiApp.View.Advice;
using FestiApp.ViewModel.Events;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.WindowsAzure.MobileServices;

namespace FestiApp.ViewModel.Advice
{
    public class AdviceEventViewModel : ViewModelBase
    {
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand ShowAddEntityCommand { get; }

        private readonly FestiMSClient _client;

        public MobileServiceCollection<FestiDB.Domain.Advice, FestiDB.Domain.Advice> Advices { get; set; }
        public FestiDB.Domain.Advice SelectedAdvice { get; set; }

        public AdviceEventViewModel(FestiMSClient client, IEditViewModel<EventViewModel> e)
        {
            ShowAddEntityCommand = new RelayCommand(ShowAddEntity);
            EditCommand = new RelayCommand(ShowEditEntity);
            DeleteCommand = new RelayCommand(DeleteEntity);
            _client = client;
            Advices = client.Advices.GetAdvice(e.Entity.Id);
            Advices.LoadMoreItemsAsync();
        }

        protected void ShowAddEntity()
        {
            var window = new BuilderAdvicePage();
            window.ShowDialog();
        }

        protected void ShowEditEntity()
        {
            var window = new BuilderAdvicePage();
            window.ShowDialog();
        }

        private async void DeleteEntity()
        {
            await _client.Advices.DeleteAsync(SelectedAdvice);
            Advices.Remove(SelectedAdvice);
        }
    }
}

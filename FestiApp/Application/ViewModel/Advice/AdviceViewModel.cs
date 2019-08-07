using FestiDB.Domain;
using GalaSoft.MvvmLight;

namespace FestiApp.ViewModel.Advice
{
    public class AdviceViewModel : ViewModelBase
    {

        public Event Event { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}

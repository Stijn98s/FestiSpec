using System.Linq;
using AutoMapper;
using FestiApp.View.Event;
using FestiDB.Domain;
using System.Windows.Input;

namespace FestiApp.ViewModel.Events
{
    public class EventListViewModel : ListViewModelBase<EventViewModel, Event>
    {
        private readonly MainViewModel _mvm;

        public EventListViewModel(IFestiClient client, IMapper mapper, MainViewModel mvm) : base(client, mapper)
        {
            _mvm = mvm;
        }

        protected override void ShowAddEntity()
        {
            var window = new AddEventPage();
            window.ShowDialog();
        }

        protected override void EditSelectedViewModel()
        {
            var window = new EditEventPage();
            window.ShowDialog();
        }

        public ICommand OpenEventCommand => _mvm.OpenEventDashboardCommand;
    }
}

using AutoMapper;
using FestiDB.Domain;
using System;

namespace FestiApp.ViewModel.Events
{
    public class ScheduleEventViewModel : ListViewModelBase<AvailabilityViewModel, Availability>
    {
        private readonly MainViewModel _mvm;

        public ScheduleEventViewModel(IFestiClient client, IMapper mapper, MainViewModel mvm) : base(client, mapper)
        {
            _mvm = mvm;
        }

        protected override void EditSelectedViewModel()
        {
            throw new NotImplementedException();
        }

        protected override void ShowAddEntity()
        {
            throw new NotImplementedException();
        }
    }
}


using AutoMapper;
using FestiApp.View.Inspector;
using FestiDB.Domain;
using GalaSoft.MvvmLight.Command;

namespace FestiApp.ViewModel.Inspectors
{
    public class InspectorListViewModel : ListViewModelBase<InspectorViewModel, Inspector>
    {
        public INetStatusService NetService { get; set; }
        public RelayCommand OpenChangePasswordCommand { get; set; }

        public InspectorListViewModel(IFestiClient client, IMapper mapper, INetStatusService statusService) : base(client, mapper)
        {
            NetService = statusService;
            OpenChangePasswordCommand = new RelayCommand(ChangePassword, CanChangePassword);
            NetService.SubscribeWithAction(OpenChangePasswordCommand.RaiseCanExecuteChanged);
            NetService.SubscribeWithAction(EditCommand.RaiseCanExecuteChanged);
        }

        private bool CanChangePassword()
        {
            return NetService.IsActive;
        }

        protected override void ShowAddEntity()
        {
            var window = new AddInspectorPage();
            window.ShowDialog();
        }

        public void ChangePassword()
        {
            var window = new ChangeInspectorPasswordWindow();
            window.ShowDialog();
        }

        protected override void EditSelectedViewModel()
        {
            var window = new EditInspectorPage();
            window.ShowDialog();
        }
    }
}

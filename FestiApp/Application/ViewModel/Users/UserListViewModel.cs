using AutoMapper;
using FestiApp.persistence;
using FestiApp.Util;
using FestiApp.View.User;
using FestiDB.Domain;
using GalaSoft.MvvmLight.CommandWpf;
using System.Linq;
using FestiDB.Domain.Roles;

namespace FestiApp.ViewModel.Users
{
    public class UserListViewModel : ListViewModelBase<UserViewModel, User>
    {
        public INetStatusService NetService { get; }
        public RelayCommand OpenChangePasswordCommand { get; set; }

        public UserListViewModel(FestiMSClient client, IMapper mapper, INetStatusService netService) : base(client, mapper)
        {
            NetService = netService;
            OpenChangePasswordCommand = new RelayCommand(ChangePassword, CanChangePassword);

            NetService.SubscribeWithAction(OpenChangePasswordCommand.RaiseCanExecuteChanged);
            NetService.SubscribeWithAction(EditCommand.RaiseCanExecuteChanged);
        }

        protected override async void Refresh()
        {
            var entities = await _client.GetSyncTable<User>().ToListAsync();
            ViewModels
                .CopyFrom(
                    entities.Where(elem => elem.Role != Role.Inspector)
                        .Select(elem =>
                            new GenericEditEntityViewModel<UserViewModel, User>(_client, _mapper, elem))
                        .ToList()
                );
        }

        private bool CanChangePassword()
        {
            return NetService.IsActive;
        }

        private void ChangePassword()
        {
            var window = new ChangePasswordWindow();
            window.Show();
        }

        protected override void ShowAddEntity()
        {
            var window = new AddUserPage();
            window.ShowDialog();
        }

        protected override void EditSelectedViewModel()
        {
            var window = new EditUserPage();
            window.ShowDialog();
        }
    }
}

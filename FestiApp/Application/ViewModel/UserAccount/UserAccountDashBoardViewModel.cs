using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AutoMapper;
using FestiApp.persistence;
using FestiApp.ViewModel.UserAccount;
using FestiDB.Domain;
using FestiApp.View;
using FestiApp.View.Event;
using FestiApp.ViewModel.Users;
using FestiDB.Persistence;
using GalaSoft.MvvmLight.Command;
using Ninject;

namespace FestiApp.ViewModel.UserAccount
{
    public class UserAccountDashBoardViewModel : GenericEditEntityViewModel<UserViewModel, User>
    {
        private readonly FestiMSClient _festiMsClient;


        public ICommand CloseCommand { get; set; }

        public UserAccountDashBoardViewModel(FestiMSClient festiMsClient, IMapper mapper, [Named("currentUser")]User currentUser) : base(festiMsClient, mapper, currentUser)
        {
            _festiMsClient = festiMsClient;

        }





    }
}


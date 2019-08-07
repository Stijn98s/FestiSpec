using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FestiDB.Domain;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace FestiApp.ViewModel.UserAccount
{
    public class UserAccountViewModel : ViewModelBase
    {
        public User User { get; set; }
    }
}

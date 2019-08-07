using FestiApp.View;
using FestiDB.Persistence;
using GalaSoft.MvvmLight.CommandWpf;
using System;

namespace FestiApp.ViewModel
{
    public interface IEditViewModel<TVM> where TVM : new()
    {
        IEntity Entity { get; }
        RelayCommand DeleteEntityCommand { get; set; }
        TVM EntityViewModel { get; }
        RelayCommand<IClosable> UpdateEntityCommand { get; set; }

        bool IsLoading { get; set; }
        bool IsNotLoading { get; }

        RelayCommand<IClosable> CloseWindowCommand { get; set; }
        void AddPredicate(Func<bool> canExecute);
    }
}
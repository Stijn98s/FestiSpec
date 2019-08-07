using FestiApp.View;
using FestiApp.ViewModel.Questions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;

namespace FestiApp.ViewModel
{
    public interface ISelectedEntityViewModel<TVM> where TVM : ViewModelBase, new()
    {
        RelayCommand DeleteEntityCommand { get; set; }
        TVM EntityViewModel { get; }
        RelayCommand<IClosable> UpdateEntityCommand { get; set; }
    }
}
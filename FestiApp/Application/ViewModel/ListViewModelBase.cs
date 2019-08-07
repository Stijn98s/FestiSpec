using AutoMapper;
using FestiApp.Util;
using FestiDB.Persistence;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Ninject;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FestiApp.ViewModel
{
    public abstract class ListViewModelBase<TViewModel, TEntity> : ViewModelBase, IAddableList<TEntity>,
        IUpdateableList<TViewModel> where TViewModel : ViewModelBase, new() where TEntity : AbstractEntity
    {

        protected IFestiClient _client;
        protected readonly IMapper _mapper;

        [Inject]
        protected ListViewModelBase(IFestiClient client, IMapper mapper)
        {
            ViewModels = new ObservableCollection<GenericEditEntityViewModel<TViewModel, TEntity>>();
            EditCommand = new RelayCommand(EditSelectedViewModel, CanEdit);
            DeleteCommand = new RelayCommand(DeleteSelectedEntity, CanDelete);
            ShowAddEntityCommand = new RelayCommand(ShowAddEntity);
            FindElement = new RelayCommand(FindElements);
            _client = client;
            _mapper = mapper;

            // ReSharper disable once VirtualMemberCallInConstructor
            Refresh();
        }

        private bool CanDelete()
        {
            if (SelectedViewModel == null) return true;
            return !SelectedViewModel.IsLoading;
        }

        private bool CanEdit()
        {
            if (SelectedViewModel == null) return true;
            return !SelectedViewModel.IsLoading;
        }

        public ICommand ShowAddEntityCommand { get; set; }

        private void DeleteSelectedEntity()
        {
            SelectedViewModel.DeleteEntityCommand.Execute(null);
            ViewModels.Remove(SelectedViewModel);
        }

        protected abstract void ShowAddEntity();
        protected abstract void EditSelectedViewModel();

        protected virtual async void Refresh()
        {
            var entities = await _client.GetSyncTable<TEntity>().ToListAsync();
            ViewModels
                .CopyFrom(
                    entities
                        .Select(elem =>
                            new GenericEditEntityViewModel<TViewModel, TEntity>(_client, _mapper, elem))
                        .ToList()
                );
        }

        public GenericEditEntityViewModel<TViewModel, TEntity> SelectedViewModel { get; set; }

        public ObservableCollection<GenericEditEntityViewModel<TViewModel, TEntity>> ViewModels { get; set; }

        public RelayCommand EditCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand FindElement { get; set; }

        public void FindElements()
        {
            throw new System.NotImplementedException();
        }

        public void AddEntity(TEntity entity)
        {
            var genericEditEntityViewModel = new GenericEditEntityViewModel<TViewModel, TEntity>(_client, _mapper, entity);
            ViewModels.Add(genericEditEntityViewModel);
        }

        public void Update(TViewModel entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
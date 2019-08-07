using AutoMapper;
using FestiApp.View;
using FestiDB.Persistence;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace FestiApp.ViewModel
{
    public abstract class GenericAddEntityViewModel<TVM, TEntity> : ViewModelBase where TEntity : AbstractEntity where TVM : ViewModelBase, new()
    {

        protected readonly IFestiClient _client;
        protected readonly IAddableList<TEntity> _list;
        protected readonly IMapper _mapper;
        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                RaisePropertyChanged();
                RaisePropertyChanged($"IsNotLoading");
            }
        }

        public bool IsNotLoading => !IsLoading;

        public TVM EntityViewModel { get; set; } = new TVM();

        protected GenericAddEntityViewModel(IAddableList<TEntity> list, IMapper mapper, IFestiClient client)
        {
            _client = client;
            _list = list;
            _mapper = mapper;
            AddEntityCommand = new RelayCommand<IClosable>(AddEntity, CanAddEntity);
            CloseWindowCommand = new RelayCommand<IClosable>(CloseWindow);
        }

        protected abstract bool CanAddEntity(IClosable window);

        public RelayCommand<IClosable> CloseWindowCommand { get; set; }

        public RelayCommand<IClosable> AddEntityCommand { get; set; }

        public virtual async void AddEntity(IClosable window)
        {
            try
            {
                _isLoading = true;
                var entity = _mapper.Map<TEntity>(EntityViewModel);
                await _client.GetSyncTable<TEntity>().InsertAsync(entity);
                _list.AddEntity(entity);
                window?.Close();
                await _client.SyncAsync();
            }
            finally
            {
                _isLoading = false;
            }
        }

        protected virtual void CloseWindow(IClosable window)
        {
            window?.Close();
        }
    }
}
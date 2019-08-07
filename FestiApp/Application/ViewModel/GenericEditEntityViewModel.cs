using AutoMapper;
using FestiApp.View;
using FestiApp.ViewModel.Questions;
using FestiDB.Persistence;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using FestiApp.Util.Util;

namespace FestiApp.ViewModel
{
    public class GenericEditEntityViewModel<TVM, TEntity> : ViewModelBase, IEditViewModel<TVM>, ISelectedEntityViewModel<TVM> where TVM : ViewModelBase, new() where TEntity : AbstractEntity
    {
        protected readonly IFestiClient _client;
        protected readonly IMapper _mapper;
        private readonly List<Func<bool>> _canUpdatePredicates;
        internal TEntity Model { get; private set; }

        public TVM EntityViewModel { get; set; } = new TVM();

        public GenericEditEntityViewModel(IFestiClient festiMsClient, IMapper mapper, TEntity model)
        {
            _canUpdatePredicates = new List<Func<bool>>();
            _client = festiMsClient;
            _mapper = mapper;
            Model = model;
            _mapper.Map(Model, EntityViewModel);
            UpdateEntityCommand = new RelayCommand<IClosable>(UpdateEntity, CanUpdate);
            DeleteEntityCommand = new RelayCommand(DeleteEntity, CanDelete);
            CloseWindowCommand = new RelayCommand<IClosable>(CloseWindow);
            Entity = model;
        }

        protected virtual bool CanDelete()
        {
            return !IsLoading;
        }

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

        protected virtual bool CanUpdate(IClosable arg)
        {
            if (IsLoading) return false;
            return _canUpdatePredicates.All(el => el.Invoke());
        }

        public IEntity Entity { get; }
        public RelayCommand DeleteEntityCommand { get; set; }

        protected async void DeleteEntity()
        {
            try
            {
                IsLoading = true;
                await _client.GetSyncTable<TEntity>().DeleteAsync(Model);
                await _client.SyncAsync();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.ToString());
            }
            finally
            {
                IsLoading = false;
            }
        }

        public RelayCommand<IClosable> UpdateEntityCommand { get; set; }



        private async void UpdateEntity(IClosable window)
        {
            try
            {
                IsLoading = true;
                Task.Run(async () =>
                {
                     await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                        {
                            window?.Close();
                        })
                    );
                }).DontAwait();
                RaisePropertyChanged("EntityViewModel");
                await _client.GetSyncTable<TEntity>().RefreshAsync(Model);
                _mapper.Map(EntityViewModel, Model);
                await _client.GetSyncTable<TEntity>().UpdateAsync(Model);
                await _client.SyncAsync();
                Model = await _client.GetSyncTable<TEntity>().LookupAsync(Model.Id);
                _mapper.Map(Model, EntityViewModel);
            }
            catch (Exception e)
            {
                
            }
            finally
            {
                IsLoading = false;
                
            }
        }

        public RelayCommand<IClosable> CloseWindowCommand { get; set; }
        public void AddPredicate(Func<bool> canExecute)
        {
            _canUpdatePredicates.Add(canExecute);
        }

        protected virtual void CloseWindow(IClosable window)
        {
            window?.Close();
        }
    }
}
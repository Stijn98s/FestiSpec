using AutoMapper;
using FestiApp.ViewModel;
using FestiDB.Domain;
using FestiDB.Domain.Answers;
using FestiDB.Domain.Table;
using FestiDB.Persistence;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SQLite;
using System.Data.SQLite;
using Ninject;
using static System.Configuration.ConfigurationManager;
using SQLiteConnection = System.Data.SQLite.SQLiteConnection;

namespace FestiApp.persistence
{
    // ReSharper disable once InconsistentNaming
    public class FestiMSClient : MobileServiceClient, IFestiClient
    {

        private MobileServiceSQLiteStore _store;
        private string _path;
        private Dictionary<Type, IMobileServiceSyncTable> _syncTables;
        internal UserRepository Users;
        [Inject]
        public IPictureRepository PictureRepository { get; set; }

        public QuestionnaireRepository Questionnaires { get; set; }
        public InspectorRepository Inspectors { get; set; }
        public AvailabilityRepository Availabilities { get; set; }
        public ContactRepository Contacts { get; internal set; }
        public AdviceRepository Advices { get; internal set; }
        public CustomerRepository Customers { get; set; }
        private readonly IMapper _mapper;

        public User CurrentUserModel { get; set; }

        public FestiMSClient(IMapper mapper, INetStatusService netStatusService) : base(AppSettings.Get("festiMSURL"))
        {
            _mapper = mapper;

            Questionnaires = new QuestionnaireRepository(this);
            Users = new UserRepository(this);
            InitSyncTables();
            CreateLocalDb();
            InitializeStore();
            Availabilities = new AvailabilityRepository(this, _syncTables[typeof(Inspector)] as IMobileServiceSyncTable<Inspector>);
            Inspectors = new InspectorRepository(_syncTables[typeof(Inspector)] as IMobileServiceSyncTable<Inspector>);
            Questionnaires = new QuestionnaireRepository(this);
            Contacts = new ContactRepository(this);
            Advices = new AdviceRepository(_syncTables[typeof(Advice)] as IMobileServiceSyncTable<Advice>);
            Customers = new CustomerRepository(this);
        }

        private void CreateLocalDb()
        {
            _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "festi.sqlite");
            if(File.Exists(_path))
                File.Delete(_path);
            SQLiteConnection.CreateFile(_path);
            
        }

        private async void InitializeStore()
        {
            if (!SyncContext.IsInitialized)
            {
                _store = new MobileServiceSQLiteStore(_path);
                var method = typeof(MobileServiceSQLiteStoreExtensions).GetMethod("DefineTable", new[] { typeof(MobileServiceSQLiteStore) });

                foreach (var syncTablesKey in _syncTables.Keys)
                {
                    var generic = method?.MakeGenericMethod(syncTablesKey);
                    generic?.Invoke(null, new object[] { _store });
                }

                await SyncContext.InitializeAsync(_store);
            }
        }

        internal void ClearStore()
        {

        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await PictureRepository.SyncAsync();
                await SyncContext.PushAsync();
                ICollection<Task> tasks = new List<Task>();

                foreach (var mobileServiceSyncTable in _syncTables)
                {
                    var createQueryInfo = typeof(IMobileServiceSyncTable<>).MakeGenericType(mobileServiceSyncTable.Key);
                    var table = mobileServiceSyncTable.Value;
                    var key = mobileServiceSyncTable.Key;
                    var methodInfo = createQueryInfo.GetMethod("CreateQuery");
                    var res = methodInfo?.Invoke(table, null);
                    var method = typeof(MobileServiceSyncTableExtensions).GetMethods()
                        .FirstOrDefault(elem =>
                            elem.Name.Equals("PullAsync") && elem.ContainsGenericParameters &&
                            elem.GetParameters().Length == 3);
                    var makeGenericMethod = method?.MakeGenericMethod(key, key);
                    tasks.Add((Task)makeGenericMethod?.Invoke(null, new[] { table, key.ToString(), res }));
                }

                await Task.WhenAll(tasks);
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    var err = exc.PushResult.Errors.FirstOrDefault();
                    if (err != null)
                    {
                        MessageBox.Show(err.RawResult);
                    }
                    syncErrors = exc.PushResult.Errors;
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                var bla = await e.Response.Content.ReadAsStringAsync();

            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine(e);
            }

            // Simple error/conflict handling.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        // Update failed, revert to server's copy
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.",
                        error.TableName, error.Item["id"]);
                    foreach (var mobileServiceTableOperationError in syncErrors)
                    {
                        Debug.WriteLine(mobileServiceTableOperationError.RawResult);
                    }
                }
            }
        }

        public async Task<T> InsertAsync<T>(T entity) where T : AbstractEntity
        {
            if (!_syncTables.ContainsKey(typeof(T))) throw new NotImplementedException("class not added to class list in FestiClient");
            await GetSyncTable<T>().InsertAsync(entity);
            return entity;
        }

        private void InitSyncTables()
        {
            _syncTables = new Dictionary<Type, IMobileServiceSyncTable>()
            {
                {typeof(Advice),GetSyncTable<Advice>()},
                {typeof(Availability),GetSyncTable<Availability>()} ,
                {typeof(Contact),GetSyncTable<Contact>()} ,
                {typeof(Customer), GetSyncTable<Customer>()} ,
                {typeof(DrawQuestion),GetSyncTable<DrawQuestion>()} ,
                {typeof(DrawQuestionAnswer),GetSyncTable<DrawQuestionAnswer>()} ,
                {typeof(Event),GetSyncTable<Event>()} ,
                {typeof(Inspector),GetSyncTable<Inspector>()} ,
                {typeof(MeasureQuestion),GetSyncTable<MeasureQuestion>()} ,
                {typeof(MeasureQuestionAnswer),GetSyncTable<MeasureQuestionAnswer>()} ,
                {typeof(MultipleChoiceQuestion),GetSyncTable<MultipleChoiceQuestion>()} ,
                {typeof(MultipleChoiceQuestionAnswer),GetSyncTable<MultipleChoiceQuestionAnswer>()} ,
                {typeof(MultipleChoiceQuestionOption),GetSyncTable<MultipleChoiceQuestionOption>()} ,
                {typeof(OpenQuestion),GetSyncTable<OpenQuestion>()} ,
                {typeof(OpenQuestionAnswer),GetSyncTable<OpenQuestionAnswer>()} ,
                {typeof(PictureQuestion),GetSyncTable<PictureQuestion>()} ,
                {typeof(PictureQuestionAnswer),GetSyncTable<PictureQuestionAnswer>()} ,
                {typeof(Questionnaire),GetSyncTable<Questionnaire>()} ,
                {typeof(ScaleQuestion),GetSyncTable<ScaleQuestion>()} ,
                {typeof(ScaleQuestionAnswer),GetSyncTable<ScaleQuestionAnswer>()} ,
                {typeof(TableQuestion),GetSyncTable<TableQuestion>()} ,
                {typeof(TableQuestionAnswer),GetSyncTable<TableQuestionAnswer>()} ,
                {typeof(TableQuestionAnswerEntry),GetSyncTable<TableQuestionAnswerEntry>()} ,
                {typeof(User),GetSyncTable<User>() } ,
                {typeof(TableQuestionMultipleColumn),GetSyncTable<TableQuestionMultipleColumn>() } ,
                {typeof(TableQuestionNumberColumn),GetSyncTable<TableQuestionNumberColumn>() } ,
                {typeof(TableQuestionStringColumn),GetSyncTable<TableQuestionStringColumn>() } ,
                {typeof(TableQuestionTimeColumn),GetSyncTable<TableQuestionTimeColumn>() },
                {typeof(CategorieQuestion),GetSyncTable<CategorieQuestion>()}
            };
        }

        public async Task LoginAsync(string username, string password)
        {
            await base.LoginAsync("custom",
                JObject.FromObject(new LoginPoco() { Password = password, Username = username }));
            await SyncAsync();
            CurrentUserModel = await Users.GetCurrentUser();
        }
    }
}
using FestiDB.Persistence;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Threading.Tasks;

namespace FestiApp.ViewModel
{
    public interface IFestiClient
    {
        IMobileServiceSyncTable<TEntity> GetSyncTable<TEntity>();
        Task SyncAsync();
        Task<TEntity> InsertAsync<TEntity>(TEntity tableQuestionStringColumn) where TEntity : AbstractEntity;
    }
}
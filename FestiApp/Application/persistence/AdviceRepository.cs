using FestiDB.Domain;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace FestiApp.persistence
{
    public class AdviceRepository
    {

        private readonly IMobileServiceSyncTable<Advice> adviceTable;

        public AdviceRepository(IMobileServiceSyncTable<Advice> adviceTable)
        {
            this.adviceTable = adviceTable;
        }

        internal MobileServiceCollection<Advice> GetAdvice(string eventId)
        {
            return new MobileServiceCollection<Advice>(adviceTable.Where(elem => elem.EventId == eventId));
        }

        internal async System.Threading.Tasks.Task DeleteAsync(Advice advice)
        {
            await adviceTable.DeleteAsync(advice);
        }
    }
}
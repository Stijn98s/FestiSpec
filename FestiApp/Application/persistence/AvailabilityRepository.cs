using FestiDB.Domain;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace FestiApp.persistence
{
    public class AvailabilityRepository
    {
        private readonly FestiMSClient _festiMsClient;
        private readonly IMobileServiceSyncTable<Inspector> _inspectorTable;

        public AvailabilityRepository(FestiMSClient festiMsClient, IMobileServiceSyncTable<Inspector> inspectorTable)
        {
            _festiMsClient = festiMsClient;
            _inspectorTable = inspectorTable;
        }
    }
}
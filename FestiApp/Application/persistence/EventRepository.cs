using FestiDB.Domain;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FestiApp.persistence
{
    public class EventRepository : IEventRepository
    {
        private readonly FestiMSClient _festiMsClient;

        public EventRepository(FestiMSClient festiMSClient)
        {
            _festiMsClient = festiMSClient;
        }

        public async Task<List<Event>> GetUpcommingEvents()
        {
            return await _festiMsClient.InvokeApiAsync<List<Event>>($"Planning/GetEvents", HttpMethod.Get, null);
        }
    }
}

using FestiDB.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FestiApp.persistence
{
    public class QuestionnaireRepository : IQuestionnaireRepository
    {

        private readonly FestiMSClient _festiMsClient;

        public QuestionnaireRepository(FestiMSClient festiMsClient)
        {
            _festiMsClient = festiMsClient;
        }

        public async Task InsertAsync<T>(T entity) where T : Inspector
        {
            await _festiMsClient.GetSyncTable<T>().InsertAsync(entity);
        }

        public async Task RemoveAsync<T>(T entity) where T : Inspector
        {
            await _festiMsClient.GetSyncTable<T>().DeleteAsync(entity);
        }

        public async Task<List<Questionnaire>> GetAll(string eventModelId)
        {
            return await _festiMsClient.GetSyncTable<Questionnaire>().Where(elem => elem.EventId == eventModelId).ToListAsync();
        }
    }
}
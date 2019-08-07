using FestiAppViewModels;
using FestiDB.Domain;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace FestiApp.persistence
{
    public class InspectorRepository
    {
        private readonly IMobileServiceSyncTable<Inspector> _inspectorTable;

        public InspectorRepository(IMobileServiceSyncTable<Inspector> festiMsClient)
        {
            _inspectorTable = festiMsClient;
        }

        public async Task AssignInspector(Inspector inspector, string questionnaireId)
        {
            try
            {
                await _inspectorTable.MobileServiceClient.InvokeApiAsync("planning/plan",
                    JObject.FromObject(new { QuestionnaireId = questionnaireId, InspectorId = inspector.Id }));
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"{e.Message} could not be fetched because application is offline.");
            }
        }

        public async Task DespatchInspector(Inspector inspector, string questionnaireId)
        {
            try
            {
                await _inspectorTable.MobileServiceClient.InvokeApiAsync("planning/unplan",
                    JObject.FromObject(new { QuestionnaireId = questionnaireId, InspectorId = inspector.Id }));
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"{e.Message} could not be fetched because application is offline.");
            }
        }

        public async Task<ICollection<InspectorDistanceViewModel>> GetAllAvailableInspectors(string id)
        {
            return await _inspectorTable.MobileServiceClient.InvokeApiAsync<ICollection<InspectorDistanceViewModel>>(
                $"Planning/Get/{id}", HttpMethod.Get, null);
        }
    }
}

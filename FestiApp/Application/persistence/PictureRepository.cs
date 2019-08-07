using System;
using FestiApp.ViewModel;
using FestiDB.Domain;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Configuration.ConfigurationManager;

namespace FestiApp.persistence
{
    public class PictureRepository : IPictureRepository
    {
        private readonly INetStatusService _netStatusService;
        private readonly FestiMSClient _client;
        private readonly string _pictureServiceUrl;
        private Queue<KeyValuePair<DrawQuestion, string>> _uploadQue;

        public PictureRepository(INetStatusService netStatusService, FestiMSClient client)
        {
            _uploadQue = new Queue<KeyValuePair<DrawQuestion, string>>();
            _netStatusService = netStatusService;
            _client = client;
            _pictureServiceUrl = AppSettings.Get("geordistorage");
        }

        public async Task SyncAsync()
        {
            while (_netStatusService.IsActive && _uploadQue.Count > 0)
            {
                var picture = _uploadQue.Dequeue();
                if (!File.Exists(picture.Value))
                {
                    await _client.GetSyncTable<DrawQuestion>().DeleteAsync(picture.Key);
                    break;
                }
                await _client.GetSyncTable<DrawQuestion>().RefreshAsync(picture.Key);
                await UploadFile(picture.Value, picture.Key);
                if (!string.IsNullOrEmpty(picture.Key.PictureUrl))
                {
                    await _client.GetSyncTable<DrawQuestion>().UpdateAsync(picture.Key);
                }
            }
        }

        public async Task UploadFileAsync(string filePath, DrawQuestion drawQuestion)
        {
            if (_netStatusService.IsActive)
            {
                await UploadFile(filePath, drawQuestion);
            }
            else
            {
                _uploadQue.Enqueue(new KeyValuePair<DrawQuestion, string>(drawQuestion, filePath));
            }
        }

        private async Task UploadFile(string filePath, DrawQuestion drawQuestion)
        {
            var fileStream = new FileStream(filePath, FileMode.Open);
            HttpContent fileStreamContent = new StreamContent(fileStream);
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(fileStreamContent, "files", Path.GetFileName(filePath));
                try
                {
                    var response = await client.PostAsync(_pictureServiceUrl, formData);
                    var res = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        _uploadQue.Enqueue(new KeyValuePair<DrawQuestion, string>(drawQuestion, filePath));
                    }

                    var res2 = await response.Content.ReadAsStringAsync();
                    drawQuestion.PictureUrl = res2.Replace("\"", "");
                }
                catch (Exception e)
                {
                    _uploadQue.Enqueue(new KeyValuePair<DrawQuestion, string>(drawQuestion, filePath));
                }
            }
        }
    }
}
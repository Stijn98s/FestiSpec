using FestiApp.Util.ViewModels.Answers;
using FestiAppViewModels;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace FestiApp.IntegrationTest
{
    [TestClass]
    public class InspectorRepositoryTest
    {

        [TestInitialize]
        public void Init()
        {
            FestiMS = new MobileServiceClient("http://localhost:60423");
        }

        public MobileServiceClient FestiMS { get; set; }

        [TestMethod]
        public async Task TestGettingInspectors()
        {
            var id = "967497a7-948f-482f-839c-0c6c12f18436";
            await FestiMS.InvokeApiAsync<ICollection<InspectorDistanceViewModel>>(
                $"Planning/Get/{id}", System.Net.Http.HttpMethod.Get, null);
        }

        [TestMethod]
        public async Task TestViewModel()
        {
            var id = "9ab31ea1-36be-4ff4-b605-2231b847afc4";
            var vm = await FestiMS.InvokeApiAsync<IEnumerable<OpenAnswerViewModel>>($"Open/GetAnswers/{id}", System.Net.Http.HttpMethod.Get, null);
        }
    }


}

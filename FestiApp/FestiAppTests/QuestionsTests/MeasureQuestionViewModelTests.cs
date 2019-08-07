using System;
using System.Threading.Tasks;
using FestiApp.NinjectModules;
using FestiApp.persistence;
using FestiApp.ViewModel.Questions;
using FestiDB.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FestiAppTests
{
    [TestClass()]
    public class MeasureQuestionTests
    {
        [TestInitialize]
        public void Init()
        {
            Question = new MeasureQuestion()
            {
                Questionnaire = new Questionnaire()
                {
                    Id = Guid.Empty.ToString()
                }
            };

            MockRepo = new Mock<IQuestionRepository>();
            MockRepo.Setup(elem => elem.InsertAsync(Question)).Returns(Task.CompletedTask);
            // MockRepoPicture.Setup(elem => elem.UploadFileAsync(""));
            MeasureVM = new MeasureQuestionViewModel(Question, MockRepo.Object);
        }

        public Mock<IQuestionRepository> MockRepo { get; set; }
        public MeasureQuestion Question { get; set; }
        public MeasureQuestionViewModel MeasureVM { get; set; }

        [TestMethod()]
        public async Task SaveTest()
        {
            await MeasureVM.Save();
            MockRepo.Verify(mock => mock.InsertAsync(Question), Times.Once);
        }
    }
}
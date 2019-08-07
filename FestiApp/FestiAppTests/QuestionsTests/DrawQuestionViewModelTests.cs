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
    public class DrawQuestionViewModelTests
    {
        [TestInitialize]
        public void Init()
        {
            Question = new DrawQuestion()
            {
                Questionnaire = new Questionnaire()
                {
                    Id = Guid.Empty.ToString()
                }
            };

            MockRepo = new Mock<IQuestionRepository>();
            MockRepoPicture = new Mock<IPictureRepository>();
            MockRepo.Setup(elem => elem.InsertAsync(Question)).Returns(Task.CompletedTask);
            // MockRepoPicture.Setup(elem => elem.UploadFileAsync(""));
            DrawVM = new DrawQuestionViewModel(Question, MockRepo.Object, MockRepoPicture.Object);
        }

        public Mock<IQuestionRepository> MockRepo { get; set; }
        public Mock<IPictureRepository> MockRepoPicture { get; set; }
        public DrawQuestion Question { get; set; }
        public DrawQuestionViewModel DrawVM { get; set; }

   
        [TestMethod()]
        public async Task SaveTest()
        {
            await DrawVM.Save();
            MockRepo.Verify(mock => mock.InsertAsync(Question), Times.Once);
        }
       
    }
}
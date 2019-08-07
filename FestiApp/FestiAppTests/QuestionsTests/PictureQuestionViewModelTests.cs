using System;
using System.Threading.Tasks;
using FestiApp.persistence;
using FestiApp.ViewModel.Questions;
using FestiDB.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FestiAppTests
{
    [TestClass()]
    public class PictureQuestionViewModelTests
    {
        [TestInitialize]
        public void Init()
        {
            Question = new PictureQuestion()
            {
                Questionnaire = new Questionnaire()
                {
                  Id = Guid.Empty.ToString()
                }
            };
            
            MockRepo = new Mock<IQuestionRepository>();
            MockRepo.Setup(elem => elem.InsertAsync(Question)).Returns(Task.CompletedTask);
            PictureVM = new PictureQuestionViewModel(Question, MockRepo.Object);
        }

        public Mock<IQuestionRepository> MockRepo { get; set; }
        public  PictureQuestion Question { get; set; }
        public PictureQuestionViewModel PictureVM { get; set; }

        [TestMethod()]
        public async Task SaveTest()
        {
            await PictureVM.Save();
            MockRepo.Verify(mock => mock.InsertAsync(Question), Times.Once);
        }
    }
}
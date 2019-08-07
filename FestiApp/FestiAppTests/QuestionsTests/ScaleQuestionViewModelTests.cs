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
    public class ScaleQuestionViewModelTests
    {
        [TestInitialize]
        public void Init()
        {
            Question = new ScaleQuestion()
            {
                Questionnaire = new Questionnaire()
                {
                    Id = Guid.Empty.ToString()
                }
            };
            MockRepo = new Mock<IQuestionRepository>();
            MockRepo.Setup(elem => elem.InsertAsync(Question)).Returns(Task.CompletedTask);
            ScalaVM = new ScaleQuestionViewModel(Question, MockRepo.Object);
        }

        public Mock<IQuestionRepository> MockRepo { get; set; }
        public ScaleQuestion Question { get; set; }
        public ScaleQuestionViewModel ScalaVM { get; set; }

        [TestMethod()]
        public async Task SaveTest()
        {
            await ScalaVM.Save();
            MockRepo.Verify(mock => mock.InsertAsync(Question), Times.Once);
        }
    }
}
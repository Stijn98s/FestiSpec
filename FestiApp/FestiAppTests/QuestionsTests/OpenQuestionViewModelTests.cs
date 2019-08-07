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
    public class OpenQuestionViewModelTests
    {
        [TestInitialize]
        public void Init()
        {
            Question = new OpenQuestion()
            {
                Questionnaire = new Questionnaire()
                {
                    Id = Guid.Empty.ToString()
                }
            };
            MockRepo = new Mock<IQuestionRepository>();
            MockRepo.Setup(elem => elem.InsertAsync(Question)).Returns(Task.CompletedTask);
            OpenVM = new OpenQuestionViewModel(Question, MockRepo.Object);
        }

        public Mock<IQuestionRepository> MockRepo { get; set; }
        public OpenQuestion Question { get; set; }
        public OpenQuestionViewModel OpenVM { get; set; }

        [TestMethod()]
        public async Task SaveTest()
        {
            await OpenVM.Save();
            MockRepo.Verify(mock => mock.InsertAsync(Question), Times.Once);
        }
    }
}
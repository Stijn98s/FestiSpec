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
    public class MultipleChoiceQuestionViewModelTests
    {
        [TestInitialize]
        public void Init()
        {
            Question = new MultipleChoiceQuestion()
            {
                Questionnaire = new Questionnaire()
                {
                    Id = Guid.Empty.ToString()
                }
            };

            MockRepo = new Mock<IQuestionRepository>();
            MockRepo.Setup(elem => elem.InsertAsync(Question)).Returns(Task.CompletedTask);
            MultiVM = new MultipleChoiceQuestionViewModel(Question, MockRepo.Object);
        }

        public Mock<IQuestionRepository> MockRepo { get; set; }
        public MultipleChoiceQuestion Question { get; set; }
        public MultipleChoiceQuestionViewModel MultiVM { get; set; }

        [TestMethod()]
        public async Task SaveTest()
        {
            await MultiVM.Save();
            MockRepo.Verify(mock => mock.InsertAsync(Question), Times.Once);
        }
    }
}
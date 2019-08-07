using System;
using System.Threading.Tasks;
using AutoMapper;
using FestiApp.NinjectModules;
using FestiApp.persistence;
using FestiApp.ViewModel.Questions;
using FestiDB.Domain;
using FestiDB.Domain.Table;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FestiAppTests
{
    [TestClass()]
    public class TableQuestionViewModelTests
    {
        [TestInitialize]
        public void Init()
        {
            Question = new TableQuestion()
            {
                Questionnaire = new Questionnaire()
                {
                    Id = Guid.Empty.ToString()
                }
            };

            MockRepo = new Mock<IQuestionRepository>();
            MockRepo.Setup(elem => elem.InsertAsync(Question)).Returns(Task.CompletedTask);
            TablaVM = new TableQuestionViewModel(Question, MockRepo.Object, new TableQuestionFactory(new FestiMSClient(new Mapper(new MapperConfiguration(
                expression => { })), new NetStatusService())));
        }

        public Mock<IQuestionRepository> MockRepo { get; set; }
        public TableQuestion Question { get; set; }
        public TableQuestionViewModel TablaVM { get; set; }

        [TestMethod()]
        public async Task SaveTest()
        {
            await TablaVM.Save();
            MockRepo.Verify(mock => mock.InsertAsync(Question), Times.Once);
        }
    }
}
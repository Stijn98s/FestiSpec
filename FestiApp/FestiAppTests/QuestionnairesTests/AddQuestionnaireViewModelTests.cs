using System;
using System.Threading.Tasks;
using AutoMapper;
using FestiApp.persistence;
using FestiApp.ViewModel;
using FestiApp.ViewModel.Events;
using FestiApp.ViewModel.Questionnaires;
using FestiApp.ViewModel.Questions;
using FestiDB.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FestiAppTests
{
    [TestClass()]
    public class AddQuestionnaireViewModelTests
    {
        private IFestiClient festiClient;

        [TestInitialize]
        public void Init()
        {

            openQ = new OpenQuestion();
            QuestMock = new Mock<IQuestionRepository>();

            _questionnaire = new Questionnaire()
            {
                Id = Guid.Empty.ToString()
            };

            OpenQuestion = new OpenQuestionViewModel(openQ, QuestMock.Object);
            QfactoryMock = new Mock<IQuestionFactory>();
            ListMock = new Mock<IAddableList<Questionnaire>>();
            MapperMock = new Mock<IMapper>();
            EditMock = new Mock<IEditViewModel<EventViewModel>>();
            festiClient = new Mock<IFestiClient>().Object;
            QfactoryMock.Setup(mock => mock.Create(_questionnaire, "Open", It.IsAny<int>())).Returns(OpenQuestion);
            EditMock.Setup(mock => mock.Entity.Id).Returns("ff");
            AddQVM = new AddQuestionnaireViewModel(QfactoryMock.Object, ListMock.Object, MapperMock.Object, EditMock.Object, festiClient);
            

            //MockRepo.Setup(elem => elem.InsertAsync(Question)).Returns(Task.CompletedTask);
            //QfactoryMock.Setup(mock => mock.QuestionTypes== "open");
            //FestiMClientMock = new Mock<FestiMSClient>();



        }

        //public Mock<FestiMSClient> FestiMClientMock { get; set; }
        public Mock<IQuestionRepository> QuestMock { get; set; }
        public Mock<IQuestionFactory> QfactoryMock { get; set; }
        public Mock<QuestionViewModel> question { get; set; }
        public AddQuestionnaireViewModel AddQVM { get; set; }
        public Mock<IAddableList<Questionnaire>> ListMock { get; set; }
        public Mock<IMapper> MapperMock {get;set ;}
        public Mock<IEditViewModel<EventViewModel>> EditMock { get; set; }
        public Questionnaire _questionnaire { get; set; }
        public Mock<Question> questions { get; set; }
        public OpenQuestionViewModel OpenQuestion { get; set; }
        public OpenQuestion openQ { get; set; }

        [TestMethod()]
        public  void AddQuestionTest()
        {
            
            AddQVM.AddQuestionAsync();

            //QfactoryMock.Verify(mock => mock.), Times.Once);
            //MockRepo.Verify(mock => mock.InsertAsync(Question), Times.Once);
            // AddQVM.AddEntityCommand.Execute(null);

        }
    }
}
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
    public class PlanInspectorViewModelTests
    {
        //private IFestiClient festiClient;

        [TestInitialize]
        public void Init()
        {
            //Inspectors = new Mock<InspectorRepository>();
            //inspector = new Inspector();
            //festiClient = new Mock<IFestiClient>().Object;
            //TableQuestionFactory tableQuestionFactory = new TableQuestionFactory();
            //Mock<IMapper> mapper = new Mock<IMapper>();
            festiClientMock = new Mock<FestiMSClient>();

            //Inspectors.Setup(mock => mock.AssignInspector(inspector, "onetwothree")).Returns(Task.CompletedTask);

            InspectionEventMock = new Mock<IEditViewModel<EventViewModel>>();
            //InspectionEventMock.Object.Entity == inspector;

            // festiClientMock.Object.Inspectors = Inspectors.Object;
            //festiClient.Setup(mock => mock.Inspectors.AssignInspector(inspector, "onetwothree")).Returns(Task.CompletedTask);
            InspectionEventMock.Setup(mock => mock.Entity.Id).Returns("ff");
            PlanInspectVM = new PlanInspectorViewModel(festiClientMock.Object, Questionaire.Object, InspectionEventMock.Object);
        }

        public PlanInspectorViewModel PlanInspectVM { get; set; }
        public Mock<IEditViewModel<QuestionnaireViewModel>> Questionaire { get; set; }
        public Mock<IEditViewModel<EventViewModel>> InspectionEventMock { get; set; }
        public Inspector inspector { get; set; }
        public Mock<InspectorRepository> Inspectors { get; set; }
        public Mock<FestiMSClient> festiClientMock { get; set; }

        [TestMethod()]
        public  void ScheduleInspectorTest()
        {
            PlanInspectVM.ScheduleInspectorIn();

            festiClientMock.Verify(mock => mock.Inspectors.AssignInspector(inspector , InspectionEventMock.Object.Entity.Id), Times.Once);


        }
        [TestMethod()]
        public  void ChangeAssign()
        {
             PlanInspectVM.ChangeAssign();
        }
        }
}
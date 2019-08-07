using System;
using System.Threading.Tasks;
using AutoMapper;
using FestiApp.persistence;
using FestiApp.View;
using FestiApp.ViewModel;
using FestiApp.ViewModel.Customers;
using FestiApp.ViewModel.Events;
using FestiApp.ViewModel.Inspectors;
using FestiApp.ViewModel.Questionnaires;
using FestiApp.ViewModel.Questions;
using FestiDB.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FestiAppTests
{
    [TestClass()]
    public class AddInspectorViewModelTests
    {
        private IFestiClient festiClient;
        private INetStatusService statusService;

        [TestInitialize]
        public void Init()
        {

            festiClient = new Mock<IFestiClient>().Object;
            statusService = new Mock<INetStatusService>().Object;
            ListMock = new InspectorListViewModel(festiClient, mapperMock.Object, statusService);
            window = new Mock<IClosable>();
            mapperMock = new Mock<IMapper>();
            //ListMock.Setup(mock => mock.ViewModels).Returns(CLASSMAKEN));
            AddQVM = new AddInspectorViewModel(new Mock<IUserRepository>().Object, ListMock, mapperMock.Object, festiClient);
            //AddQVM.l


            //MockRepo.Setup(elem => elem.InsertAsync(Question)).Returns(Task.CompletedTask);
            //QfactoryMock.Setup(mock => mock.QuestionTypes== "open");
            //FestiMClientMock = new Mock<FestiMSClient>();



        }

        //public Mock<FestiMSClient> FestiMClientMock { get; set; }
        public Mock<IClosable> window { get; set; }
        public AddInspectorViewModel AddQVM { get; set; }
        public Mock<IMapper> mapperMock { get; set; }
        public InspectorListViewModel ListMock { get; set; }

        [TestMethod()]
        public void AddInspectorTests()
        {

            AddQVM.AddEntity(window.Object);

            //QfactoryMock.Verify(mock => mock.), Times.Once);
            //MockRepo.Verify(mock => mock.InsertAsync(Question), Times.Once);
            // AddQVM.AddEntityCommand.Execute(null);

        }
    }
}
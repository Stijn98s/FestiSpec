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
    public class AddCustomerViewModelTests
    {
        private IFestiClient festiClient;

        [TestInitialize]
        public void Init()
        {
            mapperMock = new Mock<IMapper>();

            CustomervmMock = new Mock<CustomerViewModel>();
            customer = new Customer();
            customer.Id = "onetwo";
            festiClient = new Mock<IFestiClient>().Object;
            userListMock = new CustomerListViewModel(festiClient, mapperMock.Object);
            window = new Mock<IClosable>();

            //userListMock.Setup(mock => mock.ViewModels).Returns(CustomervmMock.Object, customer));
            //userListMock.Setup(mock => mock.AddEntity(customer));
            //userListMock.Setup(mock => mock.



           // mapperMock.Setup(mock => mock.Map<Customer>(customer)).Returns(customer);
            //mapperMock.Setup(mock => mock.Map(customer));
            AddQVM = new AddCustomerViewModel(userListMock, mapperMock.Object, festiClient);



            //MockRepo.Setup(elem => elem.InsertAsync(Question)).Returns(Task.CompletedTask);
            //QfactoryMock.Setup(mock => mock.QuestionTypes== "open");
            //FestiMClientMock = new Mock<FestiMSClient>();



        }

        //public Mock<FestiMSClient> FestiMClientMock { get; set; }
        public Mock<IClosable> window { get; set; }
        public AddCustomerViewModel AddQVM { get; set; }
        public Mock<IMapper> mapperMock { get; set; }
        public CustomerListViewModel userListMock { get; set; }
        public Customer customer { get; set; }
        public Mock<CustomerViewModel> CustomervmMock { get; set; }

        [TestMethod()]
        public void AddCustomerTest()
        {

            AddQVM.AddEntity(window.Object);

            // Assert.AreEqual(customer.Id, );
            mapperMock.VerifyAll();
          // mapperMock.Setup(mock=>mock.Map)
            //userListMock.Verify(mock => mock.AddEntity(customer), Times.Once);
            //QfactoryMock.Verify(mock => mock.), Times.Once);
            //MockRepo.Verify(mock => mock.InsertAsync(Question), Times.Once);
            // AddQVM.AddEntityCommand.Execute(null);

        }
    }
}
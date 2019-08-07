using System;
using System.Threading.Tasks;
using AutoMapper;
using FestiApp.persistence;
using FestiApp.ViewModel;
using FestiApp.ViewModel.Contacts;
using FestiApp.ViewModel.Customers;
using FestiDB.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.MobileServices;
using Moq;

namespace FestiAppTests
{
    [TestClass()]
    public class ContactListViewModelTests
    {
        [TestInitialize]
        public void Init()
        {
          

            CustomerMock = new Mock<IEditViewModel < CustomerViewModel>>();
            CustomerMock.Setup(el => el.Entity.Id).Returns("oneTwoTrhee");

            contact = new Contact();
            contact.Id = "oneTwoTrhee";

            ContactRepo = new Mock<IContactRepository>();
            var mobileServiceCollection = new MobileServiceCollection<Contact>(Mock.Of<IMobileServiceTableQuery<Contact>>());
            
            ContactRepo.Setup(el => el.GetContact("oneTwoTrhee")).Returns(mobileServiceCollection);
            ContactRepo.Setup(mock => mock.DeleteAsync(contact)).Returns(Task.CompletedTask);
            //ContactListVM = new ContactListViewModel();

            //ContactRepo.Setup(mock => mock..Returns(Task.CompletedTask);
            //Client = new Mock<FestiMSClient>() { CallBase = true };
            //Client.Setup(x => x.IsValid()).Returns(true);
            //Client.Setup(x => x.InsertAsync(MobileClient));
            //Client.Setup(x => x.InsertAsync(IMobileServiceClient = MobileClient);
            //MobileClient.Setup(x => x.MobileAppUriIt.IsAny<String>()));
            //ContactRepo.Setup(x => x.)
            //CustomerMock.Setup(x => x.EntityViewModel.Equals(TVM));
            //CustomerMock.Setup(el => el.Entity.Id == "22");
            //ContactRepo.Setup(el => el.)
            //ContactListVM.SelectedContact = contact;
            //contact.Setup(x => x.LastName == "ff");
            //MapperRepo = new Mock<IMapper>();
            //Client = new Mock<FestiMSClient>();
        }

        public Mock<IEditViewModel<CustomerViewModel>> CustomerMock { get; set; }
        public ContactListViewModel ContactListVM { get; set; }
        public Contact contact { get; set; }
        public Mock<IContactRepository> ContactRepo { get; set; }
        //public Mock<IMapper> MapperRepo { get; set; }
        //public Mock<FestiMSClient> Client { get; set; }
        //public CustomerViewModel TVM { get; set; }

        [TestMethod()]
        public async Task AddEnityTest()
        {
            await Task.Run(() =>
            {
            ContactListVM.AddEntity(contact);
                //check verify
                //ContactListVM.Contacts.Equals(contact);

            });
            Assert.AreEqual(contact.Id, CustomerMock.Object.Entity.Id);
        }
        [TestMethod()]
        public async Task DeleteEntityTest()
        {
            //ContactListVM.SelectedContact = contact;
            //ContactListVM.DeleteEntity();
            ContactRepo.Verify(mock => mock.DeleteAsync(contact), Times.Once);


            //CustomerMock.Verify(mock => mock.EntityViewModel.)
            //ContactRepo.Verify(mock => mock.DeleteAsync(), Times )
            //ContactListVM.DeleteCommand.Execute(null)
            //ContactRepo.VerifyAll(); //werk dit wel??
        }
    }
}
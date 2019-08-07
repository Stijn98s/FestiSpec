using FestiApp.persistence;
using FestiApp.ViewModel;
using FestiApp.ViewModel.Advice;
using FestiApp.ViewModel.Contacts;
using FestiApp.ViewModel.Customers;
using FestiApp.ViewModel.Events;
using FestiApp.ViewModel.Inspectors;
using FestiApp.ViewModel.Questionnaires;
using FestiApp.ViewModel.Questions;
using FestiApp.ViewModel.UserAccount;
using FestiApp.ViewModel.Users;
using Ninject.Modules;

namespace FestiApp.NinjectModules
{
    public class ViewModelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IFestiClient, FestiMSClient>().To<FestiMSClient>().InSingletonScope();

            Bind<QuestionFactory>().ToSelf().InSingletonScope();
            Bind<MainViewModel>().ToSelf().InSingletonScope();

            Bind<EventListViewModel>().ToSelf().InSingletonScope();
            Bind<CustomerListViewModel>().ToSelf().InSingletonScope();
            Bind<ContactListViewModel>().ToSelf().InTransientScope();
            Bind<UserListViewModel>().ToSelf().InSingletonScope();
            Bind<InspectorListViewModel>().ToSelf().InSingletonScope();
            Bind<QuestionnaireListViewModel>().ToSelf().InTransientScope();

            Bind<AdviceEventViewModel>().ToSelf().InTransientScope().Named("Transient");

            Bind<AddEventViewModel>().ToSelf().InTransientScope();
            Bind<AddCustomerViewModel>().ToSelf().InTransientScope();
            Bind<AddContactViewModel>().ToSelf().InTransientScope();
            Bind<AddUserViewModel>().ToSelf().InTransientScope();
            Bind<AddInspectorViewModel>().ToSelf().InTransientScope();
            Bind<AddQuestionnaireViewModel>().ToSelf().InTransientScope();

            Bind<EditInspectorViewModel>().ToSelf().InTransientScope();
            Bind<EditUserViewModel>().ToSelf().InTransientScope();
            Bind<EditEventViewModel>().ToSelf().InTransientScope();
            Bind<EditQuestionnaireViewModel>().ToSelf().InTransientScope();
            Bind<EditContactViewModel>().ToSelf().InTransientScope();
            Bind<EditCustomerViewModel>().ToSelf().InTransientScope();
            Bind<ChangeUserPasswordViewModel>().ToSelf().InTransientScope();

            Bind<LoginViewModel>().ToSelf().InTransientScope();
            Bind<EventDashboardViewModel>().ToSelf().InTransientScope();
            Bind<UserAccountDashBoardViewModel>().ToSelf().InTransientScope();

            Bind<ScheduleEventViewModel>().ToSelf().InSingletonScope();
            Bind<PlanInspectorViewModel>().ToSelf().InTransientScope();
        }
    }
}

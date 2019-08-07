using FestiApp.NinjectModules;
using FestiApp.persistence;
using FestiApp.ViewModel.Advice;
using FestiApp.ViewModel.Contacts;
using FestiApp.ViewModel.Customers;
using FestiApp.ViewModel.Events;
using FestiApp.ViewModel.Inspectors;
using FestiApp.ViewModel.Questionnaires;
using FestiApp.ViewModel.UserAccount;
using FestiApp.ViewModel.Users;
using FestiDB.Domain;
using Ninject;

namespace FestiApp.ViewModel
{
    public class ViewModelLocator
    {
        private readonly IKernel kernel;

        public ViewModelLocator()
        {
            kernel = new StandardKernel(new ViewModelModule(), new AutoMapperModule(), new RepositoryModule(), new ServiceModule());
            kernel.Bind<IEditViewModel<UserViewModel>>().ToMethod(context => Users.SelectedViewModel).InTransientScope();
            kernel.Bind<IEditViewModel<InspectorViewModel>>().ToMethod(context => Inspectors.SelectedViewModel).InTransientScope();

            kernel.Bind<IEditViewModel<EventViewModel>>().ToMethod(context => Events.SelectedViewModel).InTransientScope().Named("ListEv");
            kernel.Bind<IEditViewModel<CustomerViewModel>>().ToMethod(context => Customers.SelectedViewModel).InTransientScope().Named("List");
            kernel.Bind<IEditViewModel<QuestionnaireViewModel>>().ToMethod(context => LastEvent.Questions.SelectedViewModel).InTransientScope();
            kernel.Bind<IAddableList<Questionnaire>>().ToMethod(context => LastEvent.Questions);
            kernel.Bind<AdviceBuilderViewModel>().ToSelf().InTransientScope();
            kernel.Bind<AdviceEventViewModel>().ToMethod(con => LastAdvices).Named("LastAd");
            kernel.Bind<User>().ToMethod(con =>
            {
                if (con.Kernel.Get<FestiMSClient>().CurrentUserModel != null)
                {
                    return con.Kernel.Get<FestiMSClient>().CurrentUserModel;
                }
                return new User();
            }).Named("currentUser");

            kernel.Bind<IEditViewModel<ContactViewModel>>().ToMethod(el =>
            {
                return LastContacts.SelectedViewModel;
            }).InTransientScope();
        }

        public MainViewModel Main => kernel.Get<MainViewModel>();

        #region Advice


        public AdviceEventViewModel LastAdvices { get; set; }

        public AdviceBuilderViewModel AdviceBuilderViewModel => kernel.Get<AdviceBuilderViewModel>();

        public AdviceEventViewModel AdviceEventViewModel
        {
            get
            {
                LastAdvices = kernel.Get<AdviceEventViewModel>("Transient");
                return LastAdvices;
            }
        }

        #endregion

        #region Users

        public UserListViewModel Users => kernel.Get<UserListViewModel>();

        public AddUserViewModel AddUserViewModel => kernel.Get<AddUserViewModel>();

        public UserAccountDashBoardViewModel UserAccountDashBoard => kernel.Get<UserAccountDashBoardViewModel>();

        public EditUserViewModel EditUserViewModel => kernel.Get<EditUserViewModel>();

        public ChangeUserPasswordViewModel ChangeUserPasswordViewModel => kernel.Get<ChangeUserPasswordViewModel>();

        #endregion

        #region Customers

        public AddCustomerViewModel AddCustomerViewModel => kernel.Get<AddCustomerViewModel>();

        public CustomerListViewModel Customers => kernel.Get<CustomerListViewModel>();

        public EditCustomerViewModel EditCustomerViewModel => kernel.Get<EditCustomerViewModel>();

        #endregion

        #region Inspectors

        public InspectorListViewModel Inspectors => kernel.Get<InspectorListViewModel>();

        public AddInspectorViewModel AddInspectorViewModel => kernel.Get<AddInspectorViewModel>();

        public EditInspectorViewModel EditInspector => kernel.Get<EditInspectorViewModel>();

        public ChangeInspectorPasswordViewModel ChangeInspectorPasswordViewModel => kernel.Get<ChangeInspectorPasswordViewModel>();
        public PlanInspectorViewModel PlanInspectorViewModel => kernel.Get<PlanInspectorViewModel>();

        #endregion

        #region Questionnaires

        public QuestionnaireListViewModel Questionnaires => kernel.Get<QuestionnaireListViewModel>();

        public AddQuestionnaireViewModel AddQuestionnaireViewModel { get { LastAddQuestionnaire  = kernel.Get<AddQuestionnaireViewModel>();return LastAddQuestionnaire; }}

        public EditQuestionnaireViewModel EditQuestionnaireViewModel{ get { LastEditQuestionnaire = kernel.Get<EditQuestionnaireViewModel>();return LastEditQuestionnaire;}}
        #endregion

        #region Questions

        #endregion

        #region Events

        public EventListViewModel Events => kernel.Get<EventListViewModel>();

        public DashboardViewModel Dashboard => kernel.Get<DashboardViewModel>();

        public QuestionnaireListViewModel SelectedEventQuestionnaires => InspectionEvent.Questions;


        public ScheduleEventViewModel ScheduleEvent => kernel.Get<ScheduleEventViewModel>();


        public AddEventViewModel AddEventViewModel => kernel.Get<AddEventViewModel>();

        public EditEventViewModel EditEventViewModel => kernel.Get<EditEventViewModel>();


        public EventDashboardViewModel InspectionEvent
        {
            get
            {
                LastEvent = kernel.Get<EventDashboardViewModel>();
                return LastEvent;
            }
        }

        public EventDashboardViewModel LastEvent { get; set; }


        #endregion

        #region Contacts

        public AddContactViewModel AddContactViewModel => kernel.Get<AddContactViewModel>();

        public ContactListViewModel Contacts
        {
            get
            {
                LastContacts = kernel.Get<ContactListViewModel>();
                return LastContacts;
            }
        }

        public ContactListViewModel LastContacts { get; set; }

        public ContactEventViewModel EventContactsViewModel
        {
            get
            {
                return kernel.Get<ContactEventViewModel>();
            }
        }

        public EditContactViewModel EditContactViewModel => kernel.Get<EditContactViewModel>();

        #endregion

        #region InspectionEvent



        #endregion


        public LoginViewModel Login => kernel.Get<LoginViewModel>();

        public AddQuestionnaireViewModel LastAddQuestionnaire { get; private set; }

        public EditQuestionnaireViewModel LastEditQuestionnaire { get; private set; }

        public static void Cleanup()
        {

        }
    }
}
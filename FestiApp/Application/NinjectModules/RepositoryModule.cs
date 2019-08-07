using FestiApp.persistence;
using Ninject.Modules;

namespace FestiApp.NinjectModules
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPictureRepository>().To<PictureRepository>().InSingletonScope();
            Bind<IQuestionRepository>().To<QuestionRepository>().InSingletonScope();
            Bind<IContactRepository>().To<ContactRepository>().InSingletonScope();
            Bind<IEventRepository>().To<EventRepository>().InSingletonScope();
            Bind<IUserRepository>().To<UserRepository>().InSingletonScope();
            Bind<IQuestionnaireRepository>().To<QuestionnaireRepository>().InSingletonScope();
            Bind<ICustomerRepository>().To<CustomerRepository>().InSingletonScope();

        }
    }
}

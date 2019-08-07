using FestiApp.persistence;
using FestiApp.ViewModel;
using FestiApp.ViewModel.Questions;
using Ninject.Modules;

namespace FestiApp.NinjectModules
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<INetStatusService>().To<NetStatusService>().InSingletonScope();
            Bind<IQuestionFactory>().To<QuestionFactory>().InSingletonScope();
        }
    }
}

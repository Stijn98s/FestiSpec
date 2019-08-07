using AutoMapper;
using FestiApp.ViewModel.Advice;
using FestiApp.ViewModel.Contacts;
using FestiApp.ViewModel.Customers;
using FestiApp.ViewModel.Events;
using FestiApp.ViewModel.Inspectors;
using FestiApp.ViewModel.Questionnaires;
using FestiApp.ViewModel.Questions;
using FestiApp.ViewModel.UserAccount;
using FestiApp.ViewModel.Users;
using FestiDB.Domain;
using FestiDB.Domain.Table;
using Ninject;
using Ninject.Modules;

namespace FestiApp.NinjectModules
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {

            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            Bind<IMapper>().ToMethod(ctx => new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Add all profiles in current assembly
                cfg.AddProfiles(GetType().Assembly);
                cfg.CreateMap<AdviceViewModel, Advice>().ReverseMap();
                cfg.CreateMap<UserViewModel, User>().ReverseMap();
                cfg.CreateMap<UserAccountViewModel, UserAccount>().ReverseMap();
                cfg.CreateMap<CustomerViewModel, Customer>().ReverseMap();
                cfg.CreateMap<ContactViewModel, Contact>().ReverseMap();
                cfg.CreateMap<InspectorViewModel, Inspector>().ReverseMap();
                cfg.CreateMap<UserAccountDashBoardViewModel, UserAccount>();
                cfg.CreateMap<EventViewModel, Event>().ReverseMap();
                cfg.CreateMap<AvailabilityViewModel, Availability>().ReverseMap();
                cfg.CreateMap<QuestionnaireViewModel, Questionnaire>().ReverseMap();
                cfg.CreateMap<QuestionViewModel, Question>().ReverseMap();
                cfg.CreateMap<OpenQuestionViewModel, OpenQuestion>().ReverseMap();
                cfg.CreateMap<MultipleChoiceQuestionViewModel, MultipleChoiceQuestion>().ReverseMap();
                cfg.CreateMap<TableQuestionViewModel, TableQuestion>().ReverseMap();
                cfg.CreateMap<ScaleQuestionViewModel, ScaleQuestion>().ReverseMap();
                cfg.CreateMap<MeasureQuestionViewModel, MeasureQuestion>().ReverseMap();
                cfg.CreateMap<PictureQuestionViewModel, PictureQuestion>().ReverseMap();
            });

            return config;
        }
    }
}

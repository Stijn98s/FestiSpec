using AutoMapper;
using FestiApp.persistence;
using FestiApp.Util.ViewModels.Answers;
using FestiApp.View;
using FestiApp.View.Advice;
using FestiApp.ViewModel.Events;
using FestiApp.ViewModel.Questionnaires;
using FestiApp.ViewModel.Questions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace FestiApp.ViewModel.Advice
{
    public class AdviceBuilderViewModel : ViewModelBase
    {

        public FestiDB.Domain.Advice EntityViewModel { get; set; }

        public ObservableCollection<QuestionnaireViewModel> Questionnaires { get; set; } = new ObservableCollection<QuestionnaireViewModel>();

        private QuestionnaireViewModel _selectedQuestionnaire;
        public QuestionnaireViewModel SelectedQuestionnaire
        {
            get => _selectedQuestionnaire;
            set
            {
                _selectedQuestionnaire = value;
                RaisePropertyChanged("SelectedQuestionnaire");
                Task.Run(async () =>
                {
                    await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => Questions.Clear()));

                    if (value != null)
                    {
                        foreach (var question in await _questionRepository.GetAll(value.Id))
                        {
                            if (!(typeof(CategorieQuestionViewModel) == question.GetType()))
                            {
                                 await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                                new Action(() => Questions.Add(question)));
                            }
                           
                        }
                    }

                    RaisePropertyChanged("Questions");
                });
            }
        }


        public ObservableCollection<QuestionViewModel> Questions { get; set; } = new ObservableCollection<QuestionViewModel>();

        private QuestionViewModel _selectedQuestion;

        public QuestionViewModel SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                _selectedQuestion = value;
                RaisePropertyChanged("SelectedQuestion");
            }
        }

        public ICommand CloseCommand { get; set; }
        public RelayCommand<BuilderWrapper> AddQuestionCommand { get; set; }

        public ICommand DeleteCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand LabelCommand { get; set; }
        public ICommand ListViewCommand { get; set; }
        public ICommand PanelCommand { get; set; }
        public ICommand GridCommand { get; set; }
        public ICommand PictureCommand { get; set; }
        public ICommand LineChartCommand { get; set; }
        public ICommand BarChartCommand { get; set; }
        public ICommand PrevPageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }

        private readonly FestiMSClient _client;
        private readonly IQuestionRepository _questionRepository;

        public RelayCommand<IClosable> CloseWindowCommand { get; set; }

        public INetStatusService NetService { get; set; }

        public AdviceBuilderViewModel(FestiMSClient client, IMapper mapper, [Named("ListEv")]IEditViewModel<EventViewModel> e, IQuestionRepository questionRepository, INetStatusService statusService)
        {

            EntityViewModel = new FestiDB.Domain.Advice
            {
                EventId = e.Entity.Id
            };

            Task.Run(async () =>
            {
                foreach (var questionnaire in await _client.Questionnaires.GetAll(e.Entity.Id))
                {
                    Questionnaires.Add(mapper.Map<QuestionnaireViewModel>(questionnaire));
                }
                RaisePropertyChanged("Questionnaires");
            });

            _client = client;
            NetService = statusService;

            //_adviceEventList = adviceEventList;
            _questionRepository = questionRepository;

            CloseCommand = new RelayCommand<IClosable>(CloseWindow);
            AddQuestionCommand = new RelayCommand<BuilderWrapper>(AddQuestion, CanAddQuestion);

            DeleteCommand = new RelayCommand<BuilderWrapper>(Delete);
            ExportCommand = new RelayCommand<BuilderWrapper>(Export);
            LabelCommand = new RelayCommand<BuilderWrapper>(AddLabel);
            ListViewCommand = new RelayCommand<BuilderWrapper>(AddListView);
            PanelCommand = new RelayCommand<BuilderWrapper>(AddPanel);
            GridCommand = new RelayCommand<BuilderWrapper>(AddGrid);
            PictureCommand = new RelayCommand<BuilderWrapper>(AddPicture);
            LineChartCommand = new RelayCommand<BuilderWrapper>(AddLineChart);
            BarChartCommand = new RelayCommand<BuilderWrapper>(AddBarChart);
            PrevPageCommand = new RelayCommand<BuilderWrapper>(PrevPage, CanPrevPage);
            NextPageCommand = new RelayCommand<BuilderWrapper>(NextPage);

            CloseWindowCommand = new RelayCommand<IClosable>(CloseWindow);

            NetService.SubscribeWithAction(AddQuestionCommand.RaiseCanExecuteChanged);
        }

        private void CloseWindow(IClosable window)
        {
            window?.Close();
        }

        private async void AddQuestion(BuilderWrapper builderWrapper)
        {
            try
            {
                if (SelectedQuestion.GetType() == typeof(OpenQuestionViewModel))
                {
                    var result =
                        await _client.InvokeApiAsync<OpenAnswerViewModel>($"Open/GetAnswers/{SelectedQuestion.Id}",
                            HttpMethod.Get, null);
                    builderWrapper.Builder.AddGrid(result.Question, result.AnswerList);
                }

                else if (SelectedQuestion.GetType() == typeof(MultipleChoiceQuestionViewModel))
                {
                    var result =
                        await _client.InvokeApiAsync<MultipleChoiceAnswerViewModel>(
                            $"MultipleChoice/GetAnswers/{SelectedQuestion.Id}", HttpMethod.Get, null);

                    var headers = new List<string>();

                    foreach (var item in result.Options.Keys)
                    {
                        headers.Add(item);
                    }


                    var values = new List<double>();

                    foreach (var item in result.Options.Values)
                    {
                        values.Add(item);
                    }

                    builderWrapper.Builder.AddBarChart(result.Question, headers, values);
                }

                else if (SelectedQuestion.GetType() == typeof(ScaleQuestionViewModel))
                {
                    var result =
                        await _client.InvokeApiAsync<ScaleAnswerViewModel>($"Scale/GetAnswers/{SelectedQuestion.Id}",
                            HttpMethod.Get, null);

                    var headers = new List<string>();

                    foreach (var item in result.Options.Keys)
                    {
                        headers.Add(item.ToString());
                    }

                    var values = new List<double>();

                    foreach (var item in result.Options.Values)
                    {
                        values.Add(item);
                    }

                    builderWrapper.Builder.AddBarChart(result.Question, headers, values);
                }

                else if (SelectedQuestion.GetType() == typeof(MeasureQuestionViewModel))
                {
                    var result =
                        await _client.InvokeApiAsync<MeasureAnswerViewModel>(
                            $"Measure/GetAnswers/{SelectedQuestion.Id}", HttpMethod.Get, null);
                    builderWrapper.Builder.AddLabel(
                        $"{result.Question}\r\nMin: {result.Options[0]}    Avg: {result.Options[1]}    Max: {result.Options[2]}");
                }
                else if (SelectedQuestion.GetType() == typeof(TableQuestionViewModel))
                {
                    //var result = await _client.InvokeApiAsync<TableAnswerViewModel>($"Table/GetAnswers/{SelectedQuestion.Id}", HttpMethod.Get, null);
                    MessageBox.Show("Tabel vragen worden nog niet gesupport");
                }

                else if (SelectedQuestion.GetType() == typeof(PictureQuestionViewModel))
                {
                    var result = await _client.InvokeApiAsync<PictureAnswerViewModel>($"Picture/GetAnswers/{SelectedQuestion.Id}", HttpMethod.Get, null);
                    builderWrapper.Builder.AddListView(result.Question, result.UrlList);
                }

                else if (SelectedQuestion.GetType() == typeof(DrawQuestionViewModel))
                {
                    var result = await _client.InvokeApiAsync<DrawAnswerViewModel>($"Draw/GetAnswers/{SelectedQuestion.Id}", HttpMethod.Get, null);
                    builderWrapper.Builder.AddListView(result.Question, result.UrlList);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Kon de vraag niet toevoegen aan de advies bouwer. Probeer later opnieuw.");
            }

            SelectedQuestion = null;
        }

        private bool CanAddQuestion(BuilderWrapper builderWrapper)
        {
            if (SelectedQuestionnaire == null) return false;
            if (SelectedQuestion == null) return false;
            return NetService.IsActive;
        }

        private void Delete(BuilderWrapper builderWrapper)
        {
            builderWrapper.Builder.Delete();
        }

        private void Export(BuilderWrapper builderWrapper)
        {
            builderWrapper.Builder.Export();
        }

        private void AddPicture(BuilderWrapper builderWrapper)
        {
            builderWrapper.Builder.AddPicture("https://dierenplaatjes.us/geiten/geiten-plaatje-002.jpg");
        }

        private void AddLabel(BuilderWrapper builderWrapper)
        {
            builderWrapper.Builder.AddLabel("label", true);
        }

        private void AddGrid(BuilderWrapper builderWrapper)
        {
            builderWrapper.Builder.AddGrid("test", new List<string> { "kaas", "baas" });
        }

        private void AddListView(BuilderWrapper builderWrapper)
        {
            builderWrapper.Builder.AddListView("Wat is de vraag van vandaag?", new List<string>()
                {
                    "https://dierenplaatjes.us/geiten/geiten-plaatje-002.jpg",
                    "http://www.bertenernie.nl/wp-content/Images/Plaatjes/BertenErnie.nl-Bert-en-Ernie-Plaatjes-2.jpg"
                }
            );
        }

        private void AddPanel(BuilderWrapper builderWrapper)
        {
            builderWrapper.Builder.AddPanel();
        }

        private void AddLineChart(BuilderWrapper builderWrapper)
        {
            builderWrapper.Builder.AddLineChart("Question", new List<string>
            {
                "A", "B", "C", "D"
            }, new List<double>
            {
                1, 2, 5, 2
            });
        }

        private void AddBarChart(BuilderWrapper builderWrapper)
        {
            builderWrapper.Builder.AddBarChart("Question", new List<string>
            {
                "A", "B", "C", "D"
            }, new List<double>
            {
                1, 2, 5, 2
            });
        }

        private void PrevPage(BuilderWrapper builderWrapper)
        {
            builderWrapper.Builder.PrevPage();
        }

        private bool CanPrevPage(BuilderWrapper builderWrapper)
        {
            if (builderWrapper == null || builderWrapper.Builder == null) return false;
            return builderWrapper.Builder.CurrentPage > 0;
        }

        private void NextPage(BuilderWrapper builderWrapper)
        {
            builderWrapper.Builder.NextPage();
        }
    }
}

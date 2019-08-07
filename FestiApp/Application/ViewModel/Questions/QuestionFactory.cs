using FestiApp.persistence;
using FestiDB.Domain;
using FestiDB.Domain.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FestiApp.ViewModel.Questions
{
    public class QuestionFactory : IQuestionFactory
    {
        private readonly IQuestionRepository _questionRepository;

        public IEnumerable<string> QuestionTypes => _questions.OrderBy(elem => elem);

        private readonly List<string> _questions;
        private readonly IPictureRepository _pictureRepository;
        private readonly TableQuestionFactory _tableQuestionFactory;

        public QuestionFactory(IQuestionRepository questionRepository, IPictureRepository pictureRepository, TableQuestionFactory tableQuestionFactory)
        {

            _questionRepository = questionRepository;
            _pictureRepository = pictureRepository;
            _tableQuestionFactory = tableQuestionFactory;

            _questions = new List<string>
            {
                "Open",
                "Meerkeuze",
                "Foto",
                "Schaal",
                "Tabel",
                "Meet",
                "Teken",
                "Categorie"
            };
        }

        public QuestionViewModel Create(Questionnaire questionaire, string name, int count)
        {
            QuestionViewModel question;

            switch (name)
            {
                case "Categorie":
                    question = new CategorieQuestionViewModel(new CategorieQuestion()
                    {
                        Order = count,
                        Questionnaire = questionaire,
                    }, _questionRepository);
                    break;
                case "Open":
                    question = new OpenQuestionViewModel(new OpenQuestion()
                    {
                        Order = count,
                        Questionnaire = questionaire
                    }, _questionRepository);
                    break;

                case "Meerkeuze":
                    question = new MultipleChoiceQuestionViewModel(new MultipleChoiceQuestion()
                    {
                        Order = count,
                        Questionnaire = questionaire
                    }, _questionRepository);
                    break;

                case "Foto":
                    question = new PictureQuestionViewModel(new PictureQuestion()
                    {
                        Order = count,
                        Questionnaire = questionaire
                    }, _questionRepository);
                    break;

                case "Schaal":
                    question = new ScaleQuestionViewModel(new ScaleQuestion()
                    {
                        Order = count,
                        Questionnaire = questionaire
                    }, _questionRepository);
                    break;

                case "Tabel":
                    question = new TableQuestionViewModel(new TableQuestion()
                    {
                        Order = count,
                        Questionnaire = questionaire

                    }, _questionRepository, _tableQuestionFactory);
                    break;

                case "Meet":
                    question = new MeasureQuestionViewModel(new MeasureQuestion()
                    {
                        Order = count,
                        Questionnaire = questionaire
                    }, _questionRepository);
                    break;

                case "Teken":
                    question = new DrawQuestionViewModel(new DrawQuestion()
                    {
                        Order = count,
                        Questionnaire = questionaire
                    }, _questionRepository, _pictureRepository);
                    break;

                default:
                    throw new InvalidOperationException("invalid name entered in factory");
            }

            return question;
        }
    }
}

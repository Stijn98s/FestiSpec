using FestiApp.ViewModel.Questions;
using FestiDB.Domain;
using System.Collections.Generic;

namespace FestiApp.persistence
{
    public interface IQuestionFactory
    {
        IEnumerable<string> QuestionTypes { get; }

        QuestionViewModel Create(Questionnaire questionaire, string name, int count);
    }
}
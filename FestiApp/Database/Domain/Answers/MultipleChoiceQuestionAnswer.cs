namespace FestiDB.Domain.Answers
{
    public class MultipleChoiceQuestionAnswer : Answer
    {
        public virtual MultipleChoiceQuestionOption ChosenOption { get; set; }
    }
}
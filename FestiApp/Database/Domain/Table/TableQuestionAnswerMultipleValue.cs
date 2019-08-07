namespace FestiDB.Domain.Table
{
    public class TableQuestionAnswerMultipleValue : AbstractTableQuestionAnswerValue
    {
        public virtual MultipleChoiceQuestionOption AnswerValue { get; set; }
        public string AnswerValueId { get; set; }
        public override object GetValue()
        {
            return AnswerValue;
        }
    }
}
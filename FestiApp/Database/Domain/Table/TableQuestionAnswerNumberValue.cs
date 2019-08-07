namespace FestiDB.Domain.Table
{
    public class TableQuestionAnswerNumberValue : AbstractTableQuestionAnswerValue
    {
        public int AnswerValue { get; set; }

        public override object GetValue()
        {
            return AnswerValue;
        }
    }
}
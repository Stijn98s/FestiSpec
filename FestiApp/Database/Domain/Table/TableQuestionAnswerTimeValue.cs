using System;

namespace FestiDB.Domain.Table
{
    public class TableQuestionAnswerTimeValue : AbstractTableQuestionAnswerValue
    {
        public DateTime AnswerValue { get; set; }

        public override object GetValue()
        {
            return AnswerValue;
        }
    }
}
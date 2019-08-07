using System.ComponentModel.DataAnnotations;

namespace FestiDB.Domain.Table
{
    public class TableQuestionAnswerStringValue : AbstractTableQuestionAnswerValue
    {
        [MaxLength(250)]
        public string AnswerValue { get; set; }

        public override object GetValue()
        {
            return AnswerValue;
        }
    }
}
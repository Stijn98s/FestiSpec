using FestiDB.Persistence;
using Newtonsoft.Json;

namespace FestiDB.Domain.Table
{
    public class TableQuestionAnswerEntry : AbstractEntity
    {
        [JsonIgnore]
        public TableQuestionAnswer Answer { get; set; }

        public string AnswerId { get; set; }

        public virtual AbstractTableQuestionAnswerValue Key { get; set; }
        public virtual AbstractTableQuestionAnswerValue Value { get; set; }
    }
}
using FestiDB.Domain.Answers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FestiDB.Domain.Table
{
    public class TableQuestionAnswer : Answer
    {
        [ForeignKey("AnswerId")]
        public virtual ICollection<TableQuestionAnswerEntry> AnswerEntries { get; set; }
    }
}
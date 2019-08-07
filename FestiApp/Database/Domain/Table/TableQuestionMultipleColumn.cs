using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FestiDB.Domain.Table
{
    public class TableQuestionMultipleColumn : TableQuestionColumn
    {
        public virtual ICollection<MultipleChoiceQuestionOption> Options { get; set; }
    }
}
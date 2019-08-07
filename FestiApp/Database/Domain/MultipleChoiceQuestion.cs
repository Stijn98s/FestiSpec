using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FestiDB.Domain
{
    public class MultipleChoiceQuestion : Question
    {
        [ForeignKey("QuestionId")]
        public virtual ICollection<MultipleChoiceQuestionOption> Options { get; set; }
    }
}
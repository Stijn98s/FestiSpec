using System.ComponentModel.DataAnnotations;

namespace FestiDB.Domain.Answers
{
    public class OpenQuestionAnswer : Answer
    {
        [Required(AllowEmptyStrings = false), MaxLength(250)]
        public string Answer { get; set; }
    }
}
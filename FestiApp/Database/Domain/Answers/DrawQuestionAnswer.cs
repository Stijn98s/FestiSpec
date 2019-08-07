using System.ComponentModel.DataAnnotations;

namespace FestiDB.Domain.Answers
{
    public class DrawQuestionAnswer : Answer
    {
        [Required(AllowEmptyStrings = false), RegularExpression(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$")]
        public string ImageUrl { get; set; }
    }
}
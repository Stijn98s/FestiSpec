using FestiDB.Persistence;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FestiDB.Domain
{
    public class MultipleChoiceQuestionOption : AbstractEntity
    {
        [JsonIgnore]
        public MultipleChoiceQuestion Question { get; set; }

        public string QuestionId { get; set; }

        [MinLength(1), MaxLength(45)]
        public string Value { get; set; }
    }
}
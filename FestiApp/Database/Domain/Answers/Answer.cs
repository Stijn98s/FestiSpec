using FestiDB.Persistence;
using Newtonsoft.Json;

namespace FestiDB.Domain.Answers
{
    //    //TODO uncomment these
//    [JsonConverter(typeof(EntityJsonInheritanceConverter))]
    public abstract class Answer : AbstractEntity
    {
        [JsonIgnore]
        public Question Question { get; set; }

        public string QuestionId { get; set; }

        [JsonIgnore]
        public Inspector Inspector { get; set; }

        public string InspectorId { get; set; }
    }
}
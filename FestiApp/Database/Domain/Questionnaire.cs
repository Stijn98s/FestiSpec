using FestiDB.Persistence;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FestiDB.Domain
{
    public class Questionnaire : AbstractEntity
    {
        
        [ForeignKey("QuestionnaireId")]
        public ICollection<Question> Questions { get; set; }
        [JsonIgnore]
        public virtual ICollection<QuestionnaireInspector> AssignedTo { get; set; }

        [MinLength(1), MaxLength(45), RegularExpression(@"^.{1,20}$"), Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [MinLength(0), MaxLength(45)]
        public  string Theme { get; set; }

        public string EventId { get; set; }

        [JsonIgnore]
        public virtual Event Event { get; set; }

        [MinLength(0), MaxLength(10000)]
        public string Description { get; set; }
    }
}
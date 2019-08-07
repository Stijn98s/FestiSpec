using FestiDB.Domain.Answers;
using FestiDB.Persistence;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace FestiDB.Domain
{

    //    //TODO uncomment these
//    [JsonConverter(typeof(EntityJsonInheritanceConverter))]
//    [KnownType(typeof(CategorieQuestion))]
    public abstract class Question : AbstractEntity
    {

        [JsonIgnore]
        public Questionnaire Questionnaire { get; set; }

        [JsonIgnore]
        [ForeignKey("QuestionId")]
        public virtual ICollection<Answer> Answers { get; set; }

        public string QuestionnaireId { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(45), MinLength(2)]
        public string Description { get; set; }


        public int Order { get; set; }
    }
}
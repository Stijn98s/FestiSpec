using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FestiDB.Domain
{
    public class QuestionnaireInspector
    {

        public virtual Questionnaire Questionnaire { get; set; }

        [Key, Column(Order = 0)]
        public string QuestionnaireId { get; set; }


        public virtual Inspector Inspector { get; set; }

        [Key, Column(Order = 1)]
        public string InspectorId { get; set; }

        public virtual string Comment { get; set; } = "";
    }
}
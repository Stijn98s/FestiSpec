using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FestiDB.Domain
{
    public class Inspector : User
    {
        [JsonIgnore]
        [ForeignKey("InspectorId")]
        public ICollection<Availability> Availabilities { get; set; }

        public ICollection<QuestionnaireInspector> QuestionnaireInspectors { get; set; }

        public string GeodanAdresId { get; set; }

        [RegularExpression(@"^[1-9][0-9]{0,3}[a-zA-Z]{0,2}$"), Required]
        public string HouseNumber { get; set; }

        [RegularExpression(@"^[1-9][0-9]{3}\s?(?:[a-zA-Z]{2})$"), Required]
        public string PostalCode { get; set; }

        public double Locy { get; set; }
        public double Locx { get; set; }

    }
}
using FestiDB.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FestiDB.Domain
{
    public class Event : AbstractEntity
    {
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime EndDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public Customer Customer { get; set; }

        public string CustomerId { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(45), MinLength(2)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required, MaxLength(50), MinLength(2)]
        public string Location { get; set; }

        [Required, RegularExpression(@"^[1-9][0-9]{0,3}[a-zA-Z]{0,2}$")]
        public string HouseNumber { get; set; }

        [Required, RegularExpression(@"^[1-9][0-9]{3}\s?(?:[a-zA-Z]{2})$")]
        public string PostalCode { get; set; }

        public string GeodanAdresId { get; set; }

        public string GeodanAdresX { get; set; }

        public string GeodanAdresY { get; set; }

        [JsonIgnore]
        [ForeignKey("EventId")]
        public virtual ICollection<Questionnaire> Questionnaires { get; set; }

        [JsonIgnore]
        [ForeignKey("EventId")]
        public virtual ICollection<Advice> Advices { get; set; }

        [JsonIgnore]
        [ForeignKey("EventId")]
        public virtual ICollection<Availability> Availabilities { get; set; }
    }
}
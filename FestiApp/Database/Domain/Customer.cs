using FestiDB.Persistence;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FestiDB.Domain
{
    public class Customer : AbstractEntity
    {
        [JsonIgnore]
        [ForeignKey("CustomerId")]
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();

        [JsonIgnore]
        [ForeignKey("CustomerId")]
        public ICollection<Event> Events { get; set; } = new List<Event>();

        [MaxLength(45), MinLength(2), RegularExpression(@"^.+$"), Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [RegularExpression("^[0-9]{8}$"), Required(AllowEmptyStrings = false)]
        public string KvK { get; set; }

        [RegularExpression(@"^[1-9][0-9]{3}\s?(?:[a-zA-Z]{2})$"), Required(AllowEmptyStrings = false)]
        public string PostalCode { get; set; }

        [RegularExpression(@"^[1-9][0-9]{0,3}[a-zA-Z]{0,2}$"), Required(AllowEmptyStrings = false)]
        public string HouseNumber { get; set; }

        [RegularExpression(@"(^\+[0-9]{2}|^\+[0-9]{2}\(0\)|^\(\+[0-9]{2}\)\(0\)|^00[0-9]{2}|^0)([0-9]{9}$|[0-9\-\s]{10}$)"), Required(AllowEmptyStrings = false)]
        public string PhoneNumber { get; set; }
    }
}
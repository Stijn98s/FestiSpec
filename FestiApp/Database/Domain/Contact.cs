using FestiDB.Persistence;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FestiDB.Domain
{
    public class Contact : AbstractEntity
    {
        [MaxLength(200)]
        public string Note { get; set; }

        [MaxLength(45), MinLength(2), RegularExpression(@"^[\w ]+$")]
        public string FirstName { get; set; }

        [MaxLength(45), MinLength(2), RegularExpression(@"^[\w ]+$")]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(50), RegularExpression(@"^[\w ]+$")]
        public string Role { get; set; }

        [RegularExpression(@"(^\+[0-9]{2}|^\+[0-9]{2}\(0\)|^\(\+[0-9]{2}\)\(0\)|^00[0-9]{2}|^0)([0-9]{9}$|[0-9\-\s]{10}$)")]
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public Customer Customer { get; set; }

        public string CustomerId { get; set; }
    }
}
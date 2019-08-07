using FestiDB.Domain.Roles;
using FestiDB.Persistence;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace FestiDB.Domain
{
    public class User : AbstractEntity
    {
        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        [JsonIgnore]
        public UserAccount UserAccount { get; set; }

        [MaxLength(100), MinLength(2), RegularExpression(@"^[\w ]+$")]
        public string FirstName { get; set; }

        [MaxLength(100), MinLength(2), RegularExpression(@"^[\w ]+$")]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression(@"(^\+[0-9]{2}|^\+[0-9]{2}\(0\)|^\(\+[0-9]{2}\)\(0\)|^00[0-9]{2}|^0)([0-9]{9}$|[0-9\-\s]{10}$)")]
        public string Phone { get; set; }

        public Role Role { get; set; }

    }
}
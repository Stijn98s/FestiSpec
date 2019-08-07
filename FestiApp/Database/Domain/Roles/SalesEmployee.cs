using System.ComponentModel.DataAnnotations;

namespace FestiDB.Domain.Roles
{
    public class SalesEmployee : User
    {
        [Required, RegularExpression(@"^[1-9][0-9]{0,3}[a-zA-Z]{0,2}$")]
        public string HouseNumber { get; set; }

        [Required, RegularExpression(@"^[1-9][0-9]{3}\s?(?:[a-zA-Z]{2})$")]
        public string PostalCode { get; set; }
    }
}

using FestiDB.Persistence;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FestiDB.Domain
{
    public class Advice : AbstractEntity
    {
        [MinLength(2), MaxLength(45), Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [JsonIgnore]
        public Event Event { get; set; }

        public string EventId { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }
    }
}

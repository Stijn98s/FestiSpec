using FestiDB.Persistence;
using System.ComponentModel.DataAnnotations;

namespace FestiDB.Domain
{
    public class Availability : AbstractEntity
    {
        public Inspector Inspector
        {
            get; set;
        }

        public Event Event
        {
            get; set;
        }

        public string EventId { get; set; }

        public string InspectorId { get; set; }

        [Required]
        public int EventDay { get; set; } = 1;

        public bool HasResponded { get; set; }

        public bool IsAvailable { get; set; }
    }
}
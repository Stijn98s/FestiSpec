using System.ComponentModel.DataAnnotations;

namespace FestiDB.Domain
{
    public class MeasureQuestion : Question
    {
        [Required(AllowEmptyStrings = false), MaxLength(50)]
        public string Unit { get; set; }
    }
}
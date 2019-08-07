using System.ComponentModel.DataAnnotations;

namespace FestiDB.Domain
{
    public class ScaleQuestion : Question
    {
        [RegularExpression("^[0-9]+$"), Required(AllowEmptyStrings = false)]
        public int Min { get; set; }
        [RegularExpression("^[0-9]+$"), Required(AllowEmptyStrings = false)]
        public int Max { get; set; }
    }
}
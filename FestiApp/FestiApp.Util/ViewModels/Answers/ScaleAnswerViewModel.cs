using System.Collections.Generic;

namespace FestiApp.Util.ViewModels.Answers
{
    public class ScaleAnswerViewModel
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public string Question { get; set; }
        public Dictionary<int, int> Options { get; set; }

    }
}
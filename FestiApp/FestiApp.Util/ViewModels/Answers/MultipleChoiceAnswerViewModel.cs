using System.Collections.Generic;

namespace FestiApp.Util.ViewModels.Answers
{
    public class MultipleChoiceAnswerViewModel
    {
        public Dictionary<string, int> Options { get; set; }
        public string Question { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FestiApp.Util.ViewModels.Answers
{
    public class MeasureAnswerViewModel
    {
        public string Question { get; set; }
        public List<double> Options { get; set; }
    }
}
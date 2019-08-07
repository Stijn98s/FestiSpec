using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestiApp.Util.ViewModels.Answers
{
    public class TableAnswerViewModel
    {
        public string Question { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public IEnumerable<KeyValuePair<object, object>> Entries { get; set; }
    }
}

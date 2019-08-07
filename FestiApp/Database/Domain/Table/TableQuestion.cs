using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace FestiDB.Domain.Table
{

    public class TableQuestion : Question
    {


        public virtual TableQuestionColumn KeyColumn { get; set; }
        public string KeyColumnId { get; set; }


        public virtual TableQuestionColumn ValueColumn { get; set; }
        public string ValueColumnId { get; set; }

    }
}
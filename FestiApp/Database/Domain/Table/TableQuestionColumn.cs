using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using FestiDB.Persistence;
using Newtonsoft.Json;


namespace FestiDB.Domain.Table
{

//    //TODO uncomment these
//    [JsonConverter(typeof(EntityJsonInheritanceConverter))]
//    [KnownType(typeof(TableQuestionStringColumn))]
//    [KnownType(typeof(TableQuestionMultipleColumn))]
//    [KnownType(typeof(TableQuestionNumberColumn))]
//    [KnownType(typeof(TableQuestionTimeColumn))]
    public abstract class TableQuestionColumn : AbstractEntity
    {
        [Required(AllowEmptyStrings = false), MaxLength(20)]
        public string Header { get; set; }


    }
}
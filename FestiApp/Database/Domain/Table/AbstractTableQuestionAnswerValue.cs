using System;
using System.Runtime.Serialization;
using FestiDB.Persistence;
using Newtonsoft.Json;

namespace FestiDB.Domain.Table
{
    //    //TODO uncomment these
//    [JsonConverter(typeof(EntityJsonInheritanceConverter))]
//    [KnownType(typeof(TableQuestionAnswerMultipleValue))]
//    [KnownType(typeof(TableQuestionAnswerNumberValue))]
//    [KnownType(typeof(TableQuestionAnswerStringValue))]
//    [KnownType(typeof(TableQuestionAnswerTimeValue))]
    public abstract class AbstractTableQuestionAnswerValue : AbstractEntity
    {
        public abstract object GetValue();
    }
}
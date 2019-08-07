using NJsonSchema.Converters;
using System;
using System.Data.Entity.Core.Objects;

namespace FestiDB.Domain
{
    public class EntityJsonInheritanceConverter : JsonInheritanceConverter
    {
        public override string GetDiscriminatorValue(Type type)
        {
            return ObjectContext.GetObjectType(type).Name;
        }
    }
}
using System;
using System.Collections.Generic;

namespace ObjectPersister
{
    public class PropertyDefinition
    {
        public Guid Id { get; set; }
        public ObjectDefinition ObjectDefinition { get; set; }
        public string Name { get; set; }
        public PropertyType Type { get; set; }
        public bool Nullable { get; set; } = false;
        public List<String> LegalValues { get; set; }

        public PropertyDefinition(string name, ObjectDefinition def)
        {
            Id = Guid.NewGuid();
            Name = name;
            ObjectDefinition = def;
        }

        public bool IsLegal(string value)
        {
            if (value == null)
            {
                return Nullable;
            }

            switch (Type)
            {
                case PropertyType.Enum:
                    return LegalValues.Contains(value);
                //TODO: Integer range, string length
                default:
                    return true;
            }
        }
    }
}
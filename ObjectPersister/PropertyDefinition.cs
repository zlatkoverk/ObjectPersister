using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectPersister
{
    public class PropertyDefinition
    {
        public Guid Id { get; set; }
        public ObjectDefinition ObjectDefinition { get; set; }
        public string Name { get; set; }
        public PropertyType Type { get; set; }
        public bool Nullable { get; set; } = false;
        public string[] LegalValues { get; set; }

        public PropertyDefinition(string name, ObjectDefinition def)
        {
            Id = Guid.NewGuid();
            Name = name;
            ObjectDefinition = def;
        }

        public PropertyDefinition()
        {
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
                    return LegalValues.ToList().Contains(value);
                //TODO: Integer range, string length
                default:
                    return true;
            }
        }
    }
}
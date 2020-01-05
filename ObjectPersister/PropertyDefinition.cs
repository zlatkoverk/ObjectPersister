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
        public List<Enum> LegalValues { get; set; }

        public PropertyDefinition(string name)
        {
            Name = name;
        }
    }
}
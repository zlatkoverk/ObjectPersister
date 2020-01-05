using System;
using System.Collections.Generic;

namespace ObjectPersister
{
    public class ObjectDefinition
    {
        public string Name { get; set; }
        public List<PropertyDefinition> Properties { get; } = new List<PropertyDefinition>();
        public List<Object> Objects { get; } = new List<Object>();

        public ObjectDefinition(string name)
        {
            Name = name;
        }
    }
}
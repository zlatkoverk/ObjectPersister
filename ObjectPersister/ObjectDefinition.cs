using System;
using System.Collections.Generic;

namespace ObjectPersister
{
    public class ObjectDefinition
    {
        public string Name { get; set; }
        public List<PropertyDefinition> Properties { get; set; }
    }
}
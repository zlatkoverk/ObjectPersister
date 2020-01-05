using System;
using System.Collections.Generic;

namespace ObjectPersister
{
    public class Object
    {
        public Guid Id { get; set; }
        public ObjectDefinition Definition { get; set; }
        public List<Property> Properties { get; set; }
    }
}
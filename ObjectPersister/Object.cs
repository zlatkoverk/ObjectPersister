using System.Collections.Generic;

namespace ObjectPersister
{
    public class Object
    {
        public ObjectDefinition Definition { get; set; }
        public List<Property> Properties { get; set; }
    }
}
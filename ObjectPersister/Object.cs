using System;
using System.Collections.Generic;

namespace ObjectPersister
{
    public class Object
    {
        public string Name { get; set; }
        public List<Property> Properties { get; set; }
    }
}
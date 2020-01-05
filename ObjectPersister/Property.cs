using System;

namespace ObjectPersister
{
    public abstract class Property
    {
        public Guid Id { get; set; }
        public PropertyDefinition Definition { get; set; }
        public string Value { get; set; }
    }
}
using System;

namespace ObjectPersister
{
    public class Property
    {
        public Guid Id { get; set; }
        public PropertyDefinition Definition { get; set; }
        public string Value { get; set; }

        public Property(PropertyDefinition definition)
        {
            Id = Guid.NewGuid();
            Definition = definition;
        }

        public Property()
        {
        }
    }
}
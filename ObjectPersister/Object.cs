using System;
using System.Collections.Generic;

namespace ObjectPersister
{
    public class Object
    {
        public Guid Id { get; set; }
        public ObjectDefinition Definition { get; set; }
        public List<Property> Properties { get; set; } = new List<Property>();

        public Object(ObjectDefinition objectDefinition)
        {            
            Id = Guid.NewGuid();
            Definition = objectDefinition;
            Definition.Objects.Add(this);
            
            foreach (var propertyDefinition in Definition.Properties)
            {
                Properties.Add(new Property(propertyDefinition));
            }
        }

        public bool SetProperty(string name, string value)
        {
            var property = Properties.Find(p => p.Definition.Name == name);
            if (property == null) return false;

            property.Value = value;
            return true;
        }
    }
}
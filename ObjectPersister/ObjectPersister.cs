using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectPersister
{
    public class ObjectPersister
    {
        public Dictionary<string, ObjectDefinition> ObjectDefinitions { get; set; } =
            new Dictionary<string, ObjectDefinition>();

        public List<Object> Objects { get; set; } = new List<Object>();

        public ObjectDefinition DefineObject(string objectName, Dictionary<string, string> properties,
            Dictionary<string, List<string>> constraints)
        {
            if (ObjectDefinitions.ContainsKey(objectName))
            {
                throw new ArgumentException($"Object with name '{objectName}' already exists");
            }

            var def = new ObjectDefinition(objectName);

            foreach (KeyValuePair<string, string> property in properties)
            {
                var propDef = new PropertyDefinition(property.Key, def);

                propDef.Nullable = property.Value.StartsWith("?");
                var propertyType = propDef.Nullable ? property.Value.Substring(1) : property.Value;

                switch (propertyType)
                {
                    case "integer":
                        propDef.Type = PropertyType.Integer;
                        break;
                    case "string":
                        propDef.Type = PropertyType.String;
                        break;
                    case "enum":
                        propDef.Type = PropertyType.Enum;
                        propDef.LegalValues = constraints[property.Key].ToArray();
                        break;
                    default:
                        throw new NotSupportedException($"Property type '{property.Value}' is not supported");
                }

                def.Properties.Add(propDef);
            }

            ObjectDefinitions[objectName] = def;

            return def;
        }

        public Object CreateObject(string objectName, Dictionary<string, string> properties)
        {
            if (!ObjectDefinitions.ContainsKey(objectName))
            {
                throw new ArgumentException($"Object with name '{objectName}' does not exist");
            }

            var objectDefinition = ObjectDefinitions[objectName];

            var obj = new Object(objectDefinition);
            foreach (var property in properties)
            {
                if (!obj.SetProperty(property.Key, property.Value))
                    throw new ArgumentException($"Setting of property {property.Key} was not successful");
            }

            Objects.Add(obj);
            return obj;
        }

        public void DumpObjects()
        {
            foreach (var group in Objects.GroupBy(o => o.Definition))
            {
                Console.WriteLine($"{group.Key.Name}:");
                foreach (var obj in group)
                {
                    Console.WriteLine($"      {obj.Id}");
                    foreach (var objProperty in obj.Properties)
                    {
                        Console.WriteLine($"           {objProperty.Definition.Name}:{objProperty.Value}");
                    }
                }
            }
        }
    }
}
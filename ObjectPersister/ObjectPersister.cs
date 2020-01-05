using System;
using System.Collections.Generic;

namespace ObjectPersister
{
    public class ObjectPersister
    {
        public Dictionary<string, ObjectDefinition> ObjectDefinitions { get; } =
            new Dictionary<string, ObjectDefinition>();

        public List<Object> Objects { get; } = new List<Object>();

        public ObjectDefinition DefineObject(string objectName, Dictionary<string, string> properties)
        {
            if (ObjectDefinitions.ContainsKey(objectName))
            {
                throw new ArgumentException($"Object with name '{objectName}' already exists");
            }

            var def = new ObjectDefinition(objectName);

            foreach (KeyValuePair<string, string> property in properties)
            {
                var propDef = new PropertyDefinition(property.Key);

                switch (property.Value)
                {
                    case "integer":
                        propDef.Type = PropertyType.Integer;
                        break;
                    case "string":
                        propDef.Type = PropertyType.String;
                        break;
                    default:
                        throw new NotSupportedException($"Property type '{property.Value}' is not supported");
                }

                def.Properties.Add(propDef);
            }

            ObjectDefinitions[objectName] = def;

            return def;
        }
    }
}
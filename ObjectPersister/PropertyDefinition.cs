using System.Collections.Generic;

namespace ObjectPersister
{
    public class PropertyDefinition
    {
        public string Name { get; set; }
        public PropertyType Type { get; set; }
        public bool Nullable { get; set; } = false;
        public List<Enum> LegalValues { get; set; }

        public bool supports(object value)
        {
            return true;
        }
    }
}
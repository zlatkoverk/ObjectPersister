namespace ObjectPersister
{
    public abstract class Property
    {
        public PropertyDefinition Definition { get; set; }
        public dynamic Value { get; set; }
    }
}
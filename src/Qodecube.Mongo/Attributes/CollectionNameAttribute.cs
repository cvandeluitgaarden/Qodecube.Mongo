namespace Qodecube.Mongo.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class CollectionNameAttribute: Attribute
    {
        public string Name { get; set; }

        public CollectionNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}

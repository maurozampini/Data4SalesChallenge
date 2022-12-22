namespace Data4SalesChallenge.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    internal sealed class DataFieldAttribute : Attribute
    {
        public DataFieldAttribute()
        {
        }
    }
}

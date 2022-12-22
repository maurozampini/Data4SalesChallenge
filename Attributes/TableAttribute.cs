namespace Data4SalesChallenge.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class TableAttribute : Attribute
    {
        public TableAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Falta indicar el nombre de la tabla.");
            }

            Name = name;
        }

        /// <summary>
        ///     El nombre de la tabla a la que se asigna la clase.
        /// </summary>
        public string Name { get; }
    }
}

using Data4SalesChallenge.Attributes;
using Data4SalesChallenge.Model;

namespace Data4SalesChallenge.Entities
{
    public class Entity<T> where T : DataModelBase
    {
        [DataField]
        public int Count { get; set; }

        [DataField]
        public string? Next { get; set; }

        [DataField]
        public string? Previous { get; set; }

        [DataField]
        public ICollection<T>? Results { get; set; }
    }
}

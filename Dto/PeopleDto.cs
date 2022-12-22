using Data4SalesChallenge.Attributes;
using Data4SalesChallenge.Model;

namespace Data4SalesChallenge.Entities
{
    [Table("People")]
    public class PeopleDto : DataModelBase
    {
        [DataField]
        public string? Name { get; set; }

        [DataField]
        public string? Height { get; set; }

        [DataField]
        public string? Mass { get; set; }

        [DataField]
        public string? Hair_color { get; set; }

        [DataField]
        public string? Skin_color { get; set; }

        [DataField]
        public string? Eye_color { get; set; }

        [DataField]
        public string? Birth_year { get; set; }

        [DataField]
        public string? Gender { get; set; }

        [DataField]
        public string? Homeworld { get; set; }

        //[DataField]
        //public string Films { get; set; }

        //[DataField]
        //public string Species { get; set; }

        //[DataField]
        //public string Vehicles { get; set; }

        //[DataField]
        //public string Starships { get; set; }

        [DataField]
        public string? Created { get; set; }

        [DataField]
        public string? Edited { get; set; }

        [DataField]
        public string? Url { get; set; }
    }
}

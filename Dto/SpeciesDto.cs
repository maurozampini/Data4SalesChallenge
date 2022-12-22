using Data4SalesChallenge.Attributes;
using Data4SalesChallenge.Model;

namespace Data4SalesChallenge.Entities
{
    [Table("Species")]
    public class SpeciesDto : DataModelBase
    {
        [DataField]
        public string? Name { get; set; }

        [DataField]
        public string? Average_height { get; set; }

        [DataField]
        public string? Average_lifespan { get; set; }

        [DataField]
        public string? Classification { get; set; }

        [DataField]
        public string? Designation { get; set; }

        [DataField]
        public string? Eye_colors { get; set; }

        [DataField]
        public string? Hair_colors { get; set; }

        [DataField]
        public string? Homeworld { get; set; }

        [DataField]
        public string? Language { get; set; }

        //[DataField]
        //public ICollection<People>? people { get; set; }

        //[DataField]
        //public ICollection<Films>? films { get; set; }

        [DataField]
        public string? Skin_colors { get; set; }

        [DataField]
        public string? Created { get; set; }

        [DataField]
        public string? Edited { get; set; }

        [DataField]
        public string? Url { get; set; }
    }
}

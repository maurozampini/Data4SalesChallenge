using Data4SalesChallenge.Attributes;
using Data4SalesChallenge.Model;

namespace Data4SalesChallenge.Entities
{
    [Table("Planets")]
    public class Planets : DataModelBase
    {
        [PrimaryKey]
        public int PlanetID { get; set; }

        [DataField]
        public string Climate { get; set; }

        [DataField]
        public string Diameter { get; set; }

        //public ICollection<Films>? Films { get; set; }

        [DataField]
        public string Gravity { get; set; }

        [DataField]
        public string Name { get; set; }

        [DataField]
        public string Orbital_period { get; set; }

        [DataField]
        public string Population { get; set; }

        //public ICollection<People>? Residents { get; set; }

        [DataField]
        public string Rotation_period { get; set; }

        [DataField]
        public string Surface_water { get; set; }

        [DataField]
        public string Terrain { get; set; }

        [DataField]
        public string Created { get; set; }

        [DataField]
        public string Edited { get; set; }

        [DataField]
        public string Url { get; set; }
    }
}

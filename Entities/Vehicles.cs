using Data4SalesChallenge.Attributes;
using Data4SalesChallenge.Model;

namespace Data4SalesChallenge.Entities
{
    [Table("Vehicles")]
    public class Vehicles : DataModelBase
    {
        [PrimaryKey]
        public int VehiclesID { get; set; }

        [DataField]
        public string? Name { get; set; }

        [DataField]
        public string? Cargo_capacity { get; set; }

        [DataField]
        public string? Consumables { get; set; }

        [DataField]
        public string? Cost_in_credits { get; set; }

        [DataField]
        public string? Crew { get; set; }

        [DataField]
        public string? Length { get; set; }

        [DataField]
        public string? Manufacturer { get; set; }

        [DataField]
        public string? Max_atmosphering_speed { get; set; }

        [DataField]
        public string? Model { get; set; }

        [DataField]
        public string? Passengers { get; set; }

        //[DataField]
        //public ICollection<Films>? Films { get; set; }

        //[DataField]
        //public ICollection<People>? Pilots { get; set; }

        [DataField]
        public string? Vehicle_class { get; set; }

        [DataField]
        public string? Created { get; set; }

        [DataField]
        public string? Edited { get; set; }

        [DataField]
        public string? Url { get; set; }
    }
}

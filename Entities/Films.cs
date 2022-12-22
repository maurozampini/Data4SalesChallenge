﻿using Data4SalesChallenge.Attributes;
using Data4SalesChallenge.Model;

namespace Data4SalesChallenge.Entities
{
    [Table("Films")]
    public class Films : DataModelBase
    {
        [PrimaryKey]
        public int FilmsID { get; set; }
        
        [DataField]
        public int Episode_id { get; set; }

        [DataField]
        public string? Title { get; set; }

        [DataField]
        public string? Opening_crawl { get; set; }

        [DataField]
        public string? Director { get; set; }

        [DataField]
        public string? Producer { get; set; }

        [DataField]
        public string? Release_date { get; set; }

        [DataField]
        public string? Created { get; set; }

        [DataField]
        public string? Edited { get; set; }

        [DataField]
        public string? Url { get; set; }

        public ICollection<string>? Characters { get; set; }

        public ICollection<string>? Planets { get; set; }

        public ICollection<People>? People { get; set; }

        //public ICollection<Planets>? PlanetsC { get; set; }

        //public ICollection<Starships> Starships { get; set; }

        //public ICollection<Vehicles> Vehicles { get; set; }

        //public ICollection<Species> Species { get; set; }
    }
}

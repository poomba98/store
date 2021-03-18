using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

namespace apisite

{
    [Table(Name ="Instruments")]
    public class Instruments
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }
        [Column]
        public int Instrument { get; set; }
        [Column]
        public string Type { get; set; }
        [Column]
        public int Company { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public int Cost { get; set; }

    }
    [Table(Name = "Instrument")]
    public class Instrument
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }
        [Column]
        public string Name { get; set; }

    }
    [Table(Name = "Company")]
    public class Company
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }
        [Column]
        public string company { get; set; }

    }
    public class product
    {
        public int id { get; set; }
        public string instrument { get; set; }
        public string type { get; set; }
        public string company { get; set; }
        public string name { get; set; }
        public int cost { get; set; }
    }
}
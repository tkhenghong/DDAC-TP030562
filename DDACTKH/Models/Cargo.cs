using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDACTKH.Models
{
    public class Cargo
    {
        public string cargoId { get; set; }
        public string cargoName { get; set; }
        public string cargoDeparture { get; set; }
        public string cargoArrival { get; set; }
        public string cargoVolume { get; set; }
    }
}
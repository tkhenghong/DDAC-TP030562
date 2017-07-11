using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDACTKH.Models
{
    public class Schedule
    {
        public string scheduleId { get; set; }
        public List<Cargo> cargoList { get; set; }
        public List<Vessel> vesselList { get; set; }
        public List<Yard> yardList { get; set; }

        public string departureDate { get; set; }

        public string arrivalDate { get; set; }

        public string selectedCargoId { get; set; }

        public string selectedVesselName { get; set; }
        public string selectedYardName { get; set; }
    }
}
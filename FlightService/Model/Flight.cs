using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Model
{
    public class Flight : Entity
    {
        public Airport AirportDestination { get; set; }
        public Airport AirportDeparture { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public String Duration { get; set; }
        public double TicketPrice { get; set; }
        public int Capacity { get; set; }

    }
}

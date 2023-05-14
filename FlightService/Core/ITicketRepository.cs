using FlightService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Core
{
    public interface ITicketRepository
    {

        Ticket Get(string id);
        IEnumerable<Ticket> GetAll();
        void Delete(string id);
        void Update(Ticket ticket);
    }
}

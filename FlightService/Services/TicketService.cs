using FlightService.Configuration;
using FlightService.Model;
using FlightService.Model.dto;
using FlightService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Services
{
    public class TicketService : ITicketService
    {
        private readonly ProjectConfiguration _configuration;

        public TicketService(ProjectConfiguration projectConfiguration)
        {
            _configuration = projectConfiguration;

        }

        public IEnumerable<Ticket> GetAll()
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(_configuration);

                return unitOfWork.Tickets.GetAll();
            }
            catch (Exception e)
            {
                return new List<Ticket>();
            }
        }

        public Ticket Get(string Id)
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(_configuration);

                return unitOfWork.Tickets.Get(Id);
            }
            catch (Exception e)
            {
                return null;
            }
        }



        public bool Delete(string id)
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(_configuration);

                unitOfWork.Tickets.Delete(id);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Ticket Edit(TicketDTO ticketDTO)
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(_configuration);
                Ticket ticket = unitOfWork.Tickets.Get(ticketDTO.Id);

                ticket.Airport = ticketDTO.Airport;
                ticket.PassengerID = ticketDTO.PassengerID;
                ticket.IsReturning = ticketDTO.IsReturning;
                ticket.OneWayFlight = ticketDTO.OneWayFlight;
                ticket.ReturningFlight = ticketDTO.ReturningFlight;

                unitOfWork.Tickets.Update(ticket);

                return ticket;
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}

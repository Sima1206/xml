using FlightService.Configuration;
using FlightService.Core;
using FlightService.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IMongoCollection<Ticket> _ticketCollection;

        public TicketRepository(ProjectConfiguration projectConfiguration)
        {
            var mongoClient = new MongoClient(
            projectConfiguration.DatabaseConfiguration.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                projectConfiguration.DatabaseConfiguration.DatabaseName);

            _ticketCollection = mongoDatabase.GetCollection<Ticket>("ticket");
        }

        public Ticket Get(string Id)
        {
            return _ticketCollection.Find(x => x.Id == Id).FirstOrDefault();
        }

        public IEnumerable<Ticket> GetAll()
        {
            return _ticketCollection.Find(_ => true).ToList();
        }

        public void Delete(string id)
        {
            _ticketCollection.DeleteOne(x => x.Id == id);
        }

        public void Update(Ticket ticket)
        {
            _ticketCollection.ReplaceOne(x => x.Id == ticket.Id, ticket);
        }
    }
}

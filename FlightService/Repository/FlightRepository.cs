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
    public class FlightRepository : IFlightRepository
    {
        private readonly IMongoCollection<Flight> _flightCollection;

        public FlightRepository(ProjectConfiguration projectConfiguration)
        {
            var mongoClient = new MongoClient(
            projectConfiguration.DatabaseConfiguration.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                projectConfiguration.DatabaseConfiguration.DatabaseName);

            _flightCollection = mongoDatabase.GetCollection<Flight>("flight");
        }

        public Flight Get(string Id)
        {
            return _flightCollection.Find(x => x.Id == Id).FirstOrDefault();
        }

        public IEnumerable<Flight> GetAll()
        {
            return _flightCollection.Find(_ => true).ToList();
        }

        public void Delete(string id)
        {
            _flightCollection.DeleteOne(x => x.Id == id);
        }

        public void Update(Flight flight)
        {
            _flightCollection.ReplaceOne(x => x.Id == flight.Id, flight);
        }

        public void Add(Flight flight)
        {
            _flightCollection.InsertOne(flight);
        }
    }
}

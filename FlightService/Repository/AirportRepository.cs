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
    public class AirportRepository : IAirportRepository
    {
        private readonly IMongoCollection<Airport> _airportCollection;

        public AirportRepository(ProjectConfiguration projectConfiguration)
        {
            var mongoClient = new MongoClient(
            projectConfiguration.DatabaseConfiguration.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                projectConfiguration.DatabaseConfiguration.DatabaseName);

            _airportCollection = mongoDatabase.GetCollection<Airport>("airport");
        }

        public Airport Get(string Id)
        {
            return _airportCollection.Find(x => x.Id == Id).FirstOrDefault();
        }

        public IEnumerable<Airport> GetAll() 
        {
            return _airportCollection.Find(_ => true).ToList();
        }

        public void Delete(string id) 
        {
            _airportCollection.DeleteOne(x => x.Id == id);
        }

        public void Update(Airport airport) {
            _airportCollection.ReplaceOne(x => x.Id == airport.Id, airport);
        }
    }
}

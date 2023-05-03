using FlightService.Configuration;
using FlightService.Core;
using FlightService.Model;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserRepository(ProjectConfiguration projectConfiguration)
        {
            var mongoClient = new MongoClient(
            projectConfiguration.DatabaseConfiguration.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                projectConfiguration.DatabaseConfiguration.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<User>("user");
        }

        public void Add(User user)
        {
            _userCollection.InsertOne(user);
        }

        public User Get(string Id)
        {
            return _userCollection.Find(x => x.Id == Id).FirstOrDefault();
        }

        public User GetUserWithEmail(string email)
        {
            return _userCollection.Find(x => x.Email == email).FirstOrDefault();
        }
    }
}



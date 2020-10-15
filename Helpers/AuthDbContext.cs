using FindIt.Backend.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Backend.Helpers
{
    public class AuthDbContext
    {

        private readonly IMongoDatabase _database = null;

        public AuthDbContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Account> Accounts
        {
            get
            {
                return _database.GetCollection<Account>("Account");
            }
        }

    }

}


using MongoDB.Driver;
using MongoDbTest.Configration;
using MongoDbTest.Models;

namespace MongoDbTest.Services
{
    public class ServiceBase
    {
        private readonly IDatabaseSettings _databaseSettings;
        public ServiceBase(IDatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public IMongoDatabase GetMongoDb()
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            return client.GetDatabase(_databaseSettings.DatabaseName);
        }
    }
}
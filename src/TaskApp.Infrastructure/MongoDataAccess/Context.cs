namespace TaskApp.Infrastructure.MongoDataAccess
{
    using TaskApp.Infrastructure.MongoDataAccess.Entities;
    using MongoDB.Bson.Serialization;
    using MongoDB.Driver;

    public class Context
    {
        private readonly MongoClient mongoClient;
        private readonly IMongoDatabase database;

        public Context(string connectionString, string databaseName)
        {
            this.mongoClient = new MongoClient(connectionString);
            this.database = mongoClient.GetDatabase(databaseName);
            Map();
        }
        

        public IMongoCollection<Task> Tasks
        {
            get
            {
                return database.GetCollection<Task>("Tasks");
            }
        }        

        private void Map()
        {
            BsonClassMap.RegisterClassMap<Task>(cm =>
            {
                cm.AutoMap();
            });            
        }
    }
}

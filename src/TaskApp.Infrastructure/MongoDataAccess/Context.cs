﻿namespace TaskApp.Infrastructure.MongoDataAccess
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
        

        public IMongoCollection<CashFlow> Tasks
        {
            get
            {
                return database.GetCollection<CashFlow>("Tasks");
            }
        }

        public IMongoCollection<Credit> Credits
        {
            get
            {
                return database.GetCollection<Credit>("Credits");
            }
        }

        public IMongoCollection<Debit> Debits
        {
            get
            {
                return database.GetCollection<Debit>("Debits");
            }
        }

        private void Map()
        {
            BsonClassMap.RegisterClassMap<CashFlow>(cm =>
            {
                cm.AutoMap();
            });

            BsonClassMap.RegisterClassMap<Credit>(cm =>
            {
                cm.AutoMap();
            });

            BsonClassMap.RegisterClassMap<Debit>(cm =>
            {
                cm.AutoMap();
            });            
        }
    }
}

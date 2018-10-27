using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using System.Configuration;

namespace Mongo3.App_Start
{
    public class MongoDBContext
    {
        MongoClient client;
        public IMongoDatabase database;

        public MongoDBContext()
        {
            var mongoClient = new MongoClient("mongodb://ale:Root12345@mongotechealth-shard-00-00-rer0u.mongodb.net:27017,mongotechealth-shard-00-01-rer0u.mongodb.net:27017,mongotechealth-shard-00-02-rer0u.mongodb.net:27017/test?ssl=true&replicaSet=MongoTecHealth-shard-0&authSource=admin&retryWrites=true");
            //("mongodb://ale:Root12345@mongotechealth-shard-00-00-rer0u.mongodb.net:27017,mongotechealth-shard-00-01-rer0u.mongodb.net:27017,mongotechealth-shard-00-02-rer0u.mongodb.net:27017/test?ssl=true&replicaSet=MongoTecHealth&authSource=admin");
            database = mongoClient.GetDatabase(ConfigurationManager.AppSettings["MongoDBName"]);
        }
    }
}
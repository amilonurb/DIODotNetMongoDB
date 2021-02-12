using DIODotNetMongo.Data.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;

namespace DIODotNetMongo.Data
{
    public class MongoDBContext
    {
        public IMongoDatabase DB { get; set; }

        public MongoDBContext(IConfiguration configuration)
        {
            try
            {
                var settings = MongoClientSettings.FromConnectionString(configuration["ConnectionString"]);
                var client = new MongoClient(settings);
                DB = client.GetDatabase(configuration["DatabaseName"]);
                MapClasses();
            }
            catch (Exception e)
            {
                throw new MongoException("It was not possible to connect to MongoDB", e);
            }
        }

        private static void MapClasses()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };

            ConventionRegistry.Register("camelCase", conventionPack, filter => true);

            if (!BsonClassMap.IsClassMapRegistered(typeof(Infectado)))
            {
                BsonClassMap.RegisterClassMap<Infectado>(config =>
                {
                    config.AutoMap();
                    config.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}
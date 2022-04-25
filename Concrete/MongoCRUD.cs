using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using SuvillianceSystem.Connections.Infrastructure;

namespace SuvillianceSystem.Connections.Concrete
{
    public class MongoCRUD<T> : ICRUD<T> where T : IIdentifiable
    {
        public IMongoCollection<T> collection { get; set; }

        public MongoCRUD(IMongoSettings settings)
        {
            var connectionString = settings.ComposeConnectionString();
            var client = new MongoClient(connectionString);
            var dataBase = client.GetDatabase(settings.DataBaseName);
            this.collection = dataBase.GetCollection<T>(GetCollectionName(typeof(T)));
        }

        private string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public Task Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);

            try
            {
                collection.FindOneAndDelete(filter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IQueryable<T>> GetAll()
        {
            return collection.Find(new BsonDocument()).ToEnumerable();
        }

        public Task<T> GetById(string id)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
            return collection.Find(filter).SingleOrDefault();
        }

        public Task Insert(T element)
        {
            try
            {
                element.Id = element.Id == null? Guid.NewGuid().ToString():element.Id;
                collection.InsertOne(element);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task Update(string id, T replacement)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, replacement.Id);
            try
            {
                collection.FindOneAndReplace(filter, replacement);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

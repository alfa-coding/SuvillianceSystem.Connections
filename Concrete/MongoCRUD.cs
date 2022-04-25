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

        public async Task Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);

            try
            {
                await collection.FindOneAndDeleteAsync(filter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var result = await collection.FindAsync(new BsonDocument());
            return result.ToEnumerable();
        }

        public async Task<T> GetById(string id)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
            var filtered = await collection.FindAsync(filter);
            var result = filtered.SingleOrDefault();
            return result;
        }

        public async Task Insert(T element)
        {
            try
            {
                element.Id = element.Id == null ? Guid.NewGuid().ToString() : element.Id;
                await collection.InsertOneAsync(element);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(string id, T replacement)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, replacement.Id);
            try
            {
                await collection.FindOneAndReplaceAsync(filter, replacement);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

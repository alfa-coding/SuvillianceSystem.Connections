using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuvillianceSystem.Connections.Concrete
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)] // This class is used for linking the Mongo tables to the models in our project
    public class BsonCollectionAttribute : Attribute
    {
        public string CollectionName { get; }

        public BsonCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}

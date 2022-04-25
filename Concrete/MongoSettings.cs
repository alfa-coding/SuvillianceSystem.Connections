using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace SuvillianceSystem.Connections.Infrastructure
{
    public class MongoSettings : IMongoSettings
    {
        
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
        public string DataBaseName { get; set; }

        public string ComposeConnectionString()
        {
            if (String.IsNullOrEmpty(this.UserName))
            {
                return $@"mongodb://{this.HostName}:{this.Port}";
            }
            return $@"mongodb://{this.UserName}:{this.Password}@{this.HostName}:{this.Port}";
        }
    }

}
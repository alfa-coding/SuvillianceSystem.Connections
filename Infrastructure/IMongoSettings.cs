using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace SuvillianceSystem.Connections.Infrastructure
{
    public interface IMongoSettings
    {
        string HostName { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string Port { get; set; }
        public string DataBaseName { get; set; }

        string ComposeConnectionString();

    }

}
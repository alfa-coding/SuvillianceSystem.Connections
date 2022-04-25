using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace SuvillianceSystem.Connections.Infrastructure
{
    public interface ICRUD<T> where T : IIdentifiable
    {
        Task Insert(T element);

        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(string id);

        Task Update(string id, T replacement);

        Task Delete(string id);

    }

}
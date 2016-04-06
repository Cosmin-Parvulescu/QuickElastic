using System.Collections.Generic;
using System.Threading.Tasks;
using QuickElastic.Core.Domain;

namespace QuickElastic.Core.DataProviders
{
    public interface IDataProvider<out T>
    {
        Task<IEnumerable<User>> GetData();
    }
}

using System.Collections;
using System.Collections.Generic;

namespace QuickElastic.Data
{
    public interface IElasticSearcher<out T>
    {
        IEnumerable<T> Search(string query);
    }
}

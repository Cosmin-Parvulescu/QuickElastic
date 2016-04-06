using System.Collections.Generic;

namespace QuickElastic.Data
{
    public interface IElasticIndexer<in T>
    {
        void Index(IEnumerable<T> entities);
    }
}

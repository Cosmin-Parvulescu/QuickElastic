using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using QuickElastic.Data.SearchEntities;

namespace QuickElastic.Data
{
    public class UserElasticSearcher : IElasticSearcher<UserSearchEntity>
    {
        private readonly ElasticClient _elasticClient;

        public UserElasticSearcher()
        {
            var node = new Uri("http://localhost:9200");
            var connectionPool = new SingleNodeConnectionPool(node);

            var settings = new ConnectionSettings(connectionPool);
            settings.DisableDirectStreaming();

            _elasticClient = new ElasticClient(settings);
        }

        public IEnumerable<UserSearchEntity> Search(string query)
        {
            var fuzzyQuery =
                _elasticClient.Search<UserSearchEntity>(
                    sd => sd.Query(qcd => qcd.Fuzzy(fqd => fqd.Field(Field.Create("_all")).Value(query))));

            return fuzzyQuery.Documents;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using QuickElastic.Core.Domain;
using QuickElastic.Data.SearchEntities;

namespace QuickElastic.Data
{
    public class UserElasticIndexer : IElasticIndexer<User>
    {
        private readonly ElasticClient _elasticClient;

        public UserElasticIndexer()
        {
            var node = new Uri("http://localhost:9200");
            var connectionPool = new SingleNodeConnectionPool(node);
            
            var settings = new ConnectionSettings(connectionPool);

            _elasticClient = new ElasticClient(settings);
        }

        public async void Index(IEnumerable<User> users)
        {
            var userSearchEntities = users
                .Select(user => new UserSearchEntity
                {
                    Id = user.Id,
                    Name = string.Format("{0} {1}", user.FirstName, user.LastName),
                    Username = user.Username,
                    Email = user.Email
                });

            await _elasticClient.IndexManyAsync(userSearchEntities, "quickelastic");
        }
    }
}

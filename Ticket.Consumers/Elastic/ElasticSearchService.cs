using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.Common.Enums;
using Ticket.Consumers.Configuration;

namespace Ticket.Consumers.Elastic
{
    public class ElasticSearchService<T> : IElasticSearchService<T>
        where T : class, new()
    {
        ElasticClient _client;
        public ElasticSearchService()
        {
            _client = CreateClient();
        }
        public async Task<bool> CheckExistsAndInsert(T model, IndexNames indexName)
        {
            try
            {
                if (string.IsNullOrEmpty(indexName.ToString()) || model == null)
                    throw new ArgumentNullException($"{nameof(indexName)} or {nameof(model)} null");
                else
                {
                    if (!_client.Indices.Exists(indexName.ToString()).Exists)
                    {
                        var indexSettings = new IndexSettings();
                        indexSettings.NumberOfReplicas = 1;
                        indexSettings.NumberOfShards = 3;

                       await _client.Indices.CreateAsync(indexName.ToString(), index =>
                          index.Map<T>(m => m.AutoMap()
                                 )
                         .InitializeUsing(new IndexState() { Settings = indexSettings })
                         .Aliases(a => a.Alias(indexName.ToString())));
                    }
                    IndexResponse response = await _client.IndexAsync<T>(model, idx => idx.Index(indexName.ToString()));
                    return response.IsValid;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        private ElasticClient CreateClient()
        {
            var connectionSettings = new ConnectionSettings(new Uri(ElasticConfig.ConnectionString))
                .DisablePing()
                .DisableDirectStreaming(true)
                .SniffOnStartup(false)
                .SniffOnConnectionFault(false)
                 .BasicAuthentication(ElasticConfig.UserName, ElasticConfig.Password);

            return new ElasticClient(connectionSettings);
        }
        public ElasticClient CreateClientWithIndex(string defaultIndex)
        {
            var connectionSettings = new ConnectionSettings(new Uri(ElasticConfig.ConnectionString))
                .DisablePing()
                .SniffOnStartup(false)
                .SniffOnConnectionFault(false)
                .DefaultIndex(defaultIndex)
             .BasicAuthentication(ElasticConfig.UserName, ElasticConfig.Password);
            return new ElasticClient(connectionSettings);
        }
    }
}

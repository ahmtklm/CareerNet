using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;

namespace CareerNetJob.DataAccess.Clients.Abstractions
{
    /// <summary>
    /// ElasticSearch ile alakalı işlemleri barındıran sözleşme
    /// </summary>
    public interface IElasticSearchClientRepo<T>
    {
        Task<IndexResponse> InsertDocumentAsync(T entity, string indexName);
        Task<bool> CheckIndexAsync(string indexName);
        Task<CreateIndexResponse> CreateIndexAsync(string indexName);
        Task<List<T>> GetAllDocuments(string indexName);
        Task<List<T>> GetAllDocumenstByExpiredDateAsync(string indexName, string expiredDate);
    }
}

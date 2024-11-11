using CareerNetJob.DataAccess.Clients.Abstractions;
using CareerNetJob.DataAccess.EntityModels;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;
using System.Globalization;

namespace CareerNetJob.DataAccess.Clients.Concretes
{
    /// <summary>
    /// Elasticsearch client sınıfının concrete metodları
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ElasticSearchClientRepo<T>(ElasticsearchClient elasticsearchClient) : IElasticSearchClientRepo<T> where T : BaseEntityModel
    {
        private readonly ElasticsearchClient _elasticsearchClient = elasticsearchClient;

        /// <summary>
        /// ElasticSearch indekse yeni veriler kaydeder.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task<IndexResponse> InsertDocumentAsync(T entity, string indexName)
        {
            var indexReponse = await _elasticsearchClient.IndexAsync(entity, x => x.Index(indexName));
            return indexReponse;
        }

        /// <summary>
        /// Indeks ismini var olup olmamasını kontrol eder.
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task<bool> CheckIndexAsync(string indexName)
        {
            var isExistIndex = await _elasticsearchClient.Indices.ExistsAsync(indexName);
            return isExistIndex.Exists;
        }

        /// <summary>
        /// İndeks oluşturur
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task<CreateIndexResponse> CreateIndexAsync(string indexName)
        {
            var response = await _elasticsearchClient.Indices.CreateAsync(indexName);
            return response;
        }

        /// <summary>
        /// İndekse göre verileri search eder.
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task<List<T>> GetAllDocuments(string indexName)
        {
            var result = await _elasticsearchClient.SearchAsync<T>(s => s.Index(indexName)
                                                                         .Sort(sort => sort
                                                                         .Field(f => f.PostedDate, p => p.Order(SortOrder.Desc))));

            return [.. result.Documents];
        }

        /// <summary>
        /// İlanın yayında kalma süresine göre search eder.
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="expiredDate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<T>> GetAllDocumenstByExpiredDateAsync(string indexName, string expiredDate)
        {
            if (!DateTime.TryParseExact(expiredDate, "yyyy-MM-ddTHH:mm:ss", null, DateTimeStyles.None, out DateTime dateTimeOffset))
                throw new Exception("Datetime convert ederken hata meydana geldi!");

            var searchResponse = await _elasticsearchClient.SearchAsync<T>(s => s.Index(indexName).Size(100)
                 .Query(q => q
                     .Match(m => m
                         .Field(f => f.ExpireDate)
                             .Query(dateTimeOffset))));

            if (!searchResponse.IsValidResponse) throw new Exception(searchResponse.DebugInformation);

            return [.. searchResponse.Documents];
        }
    }
}

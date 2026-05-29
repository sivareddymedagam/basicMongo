using BasicMongo.Models;
using BasicMongo.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BasicMongo.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _products = database.GetCollection<Product>(settings.Value.CollectionName);
        }

        // GET ALL
        public async Task<List<Product>> GetAllAsync() =>
            await _products.Find(_ => true).ToListAsync();

        // GET BY ID
        public async Task<Product?> GetByIdAsync(string id) =>
            await _products.Find(p => p.Id == id).FirstOrDefaultAsync();

        // CREATE
        public async Task CreateAsync(Product product) =>
            await _products.InsertOneAsync(product);

        // UPDATE
        public async Task UpdateAsync(string id, Product updated) =>
            await _products.ReplaceOneAsync(p => p.Id == id, updated);

        // DELETE
        public async Task DeleteAsync(string id) =>
            await _products.DeleteOneAsync(p => p.Id == id);
    }
}

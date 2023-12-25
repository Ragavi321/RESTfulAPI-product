using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public class ProductService : IProductService
    {
        private readonly DatabaseSettings _dbsettings;
        private readonly IMongoCollection<Product> _productCollection;

        public ProductService(IOptions<DatabaseSettings> dbsettings)
        {
            _dbsettings = dbsettings.Value;
            var mongoClient = new MongoClient(dbsettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbsettings.Value.DatabaseName);
            _productCollection = mongoDatabase.GetCollection<Product>(dbsettings.Value.CollectionName);
        }

        /*public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await _productCollection.Find(_ => true).ToListAsync();
            return products;
        }*/

        //Oneline version of the same function 
        public async Task<IEnumerable<Product>> GetAllAsync() => await _productCollection.Find(product => true).ToListAsync();

        public async Task<Product> GetById(string Id) => await _productCollection.Find(product => product.Id == Id).FirstOrDefaultAsync();
        
        public async Task CreateAsync(Product product) => await _productCollection.InsertOneAsync(product);

        public async Task UpdateAsync(string id, Product product) => await _productCollection.ReplaceOneAsync(product => product.Id == id, product);

        public async Task DeleteAsync(string id) => await _productCollection.DeleteOneAsync(product => product.Id == id);
    }
}

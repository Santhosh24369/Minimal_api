using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace Minimal_api.Services
{
    public class JobServices
    {
        private readonly IMongoCollection<JobModel> _booksCollection;

        public JobServices(
        IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _booksCollection = mongoDatabase.GetCollection<JobModel>(
                bookStoreDatabaseSettings.Value.BooksCollectionName);
        }

        public async Task<List<JobModel>> GetAsync() =>
        await _booksCollection.Find(_ => true).ToListAsync();

        public async Task<JobModel?> GetAsync(string id) =>
            await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(JobModel newBook) =>
            await _booksCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, JobModel updatedBook) =>
            await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _booksCollection.DeleteOneAsync(x => x.Id == id);
    }
}

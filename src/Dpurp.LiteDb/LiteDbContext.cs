using LiteDB;

namespace Dpurp.LiteDb
{
    public class LiteDbContext
    {
        private readonly LiteDatabase _db;

        public LiteDbContext(string connectionString)
        {
            _db = new LiteDatabase(connectionString);
        }

        public LiteCollection<TItem> Set<TItem>()
        {
            return _db.GetCollection<TItem>();
        }
    }
}

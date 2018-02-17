using SQLite;

namespace investingph.Services
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}

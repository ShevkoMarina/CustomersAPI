using System.Data.SQLite;

namespace HSEApiTraining.Providers
{
    public interface ISQLiteConnectionProvider
    {
        SQLiteConnection GetDbConnection();
    }
}

using Dapper;
using HSEApiTraining.Models.Options;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace HSEApiTraining.Providers
{
    public class SQLiteConnectionProvider : ISQLiteConnectionProvider
    {
        private readonly string _connectionString;

        public SQLiteConnectionProvider(IOptions<DbConnectionOptions> dbOptions)
        {
            _connectionString = dbOptions.Value.ConnectionString;
            using (var connection = GetDbConnection())
            {
                var createCustomerTableCommand =
                   @"CREATE TABLE IF NOT EXISTS customer
                   (
                     id                             integer primary key AUTOINCREMENT,
                     name                           varchar(100) not null,
                     surname                        varchar(100),
                     phone_number                   varchar(100)
                   )";
                connection.Execute(createCustomerTableCommand);

                var createBannedPhoneTableCommand =
                   @"CREATE TABLE IF NOT EXISTS banned_phone
                   (
                     id                             integer primary key AUTOINCREMENT,
                     phone                          varchar(100) 
                   )";
                connection.Execute(createBannedPhoneTableCommand);
            }
        }

        public SQLiteConnection GetDbConnection()
            => new SQLiteConnection(_connectionString);
    }
}

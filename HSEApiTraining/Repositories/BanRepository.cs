using Dapper;
using HSEApiTraining.Models.Ban;
using HSEApiTraining.Providers;
using System;
using System.Collections.Generic;

namespace HSEApiTraining
{
    public class BanRepository : IBanRepository
    {
        private readonly ISQLiteConnectionProvider _connectionProvider;

        public BanRepository(ISQLiteConnectionProvider sqliteConnectionProvider)
        {
            _connectionProvider = sqliteConnectionProvider;
        }

        public string AddBannedPhone(string bannedPhone)
        {
            try
            {
                string error = null;
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();

                    var exists = connection.Execute(
                        @"INSERT INTO banned_phone ( phone ) 
                          SELECT @Phone
                          WHERE NOT EXISTS(SELECT 1 FROM banned_phone WHERE phone = @Phone);",
                        new { Phone = bannedPhone });

                    if (exists == 0)
                        error = $"Phone '{bannedPhone}' is already in a table";
                }
                return error;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string DeleteAll()
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    connection.Execute(@"DELETE FROM banned_phone");
                    connection.Execute(@"DELETE FROM sqlite_sequence WHERE name = 'banned_phone'");
                }
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string DeletePhone(int id)
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    var count = connection.Execute(
                        @"DELETE FROM banned_phone WHERE id == @id",
                        new { id });

                    if (count == 0)
                        return $"Phone with id '{id}' doesn't exist in a table";
                }
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public (string Error, IEnumerable<BannedPhone> Phones) GetBannedPhones()
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    return (
                        null,
                        connection.Query<BannedPhone>(
                            @"SELECT 
                            id as Id,
                            phone as Phone
                            FROM banned_phone"));
                }
            }
            catch (Exception e)
            {
                return (e.Message, null);
            }
        }
    }
}

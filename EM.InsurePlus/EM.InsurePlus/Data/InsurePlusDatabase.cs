

namespace EM.InsurePlus.Data
{
    using SQLite;
    using DO = Models.DataModels;
    public class InsurePlusDatabase
    {
        private readonly SQLiteAsyncConnection _connection;
        public InsurePlusDatabase(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);

            _connection.CreateTableAsync<DO.VehiclePolicy>().Wait();
        }
    }
}

using AutoMapper;
using EM.InsurePlus.Common;
using EM.InsurePlus.Data.Repositories.Contracts;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EM.InsurePlus.Data.Repositories
{
    using DO = Models.DataModels;
    using SO = Models.ServiceModels;
    public class VehiclePolicyRepository : IVehiclePolicyRepository
    {
        private readonly SQLiteAsyncConnection _connection;
        public VehiclePolicyRepository()
        {
            _connection = new SQLiteAsyncConnection(Preferences.Get(PreferencesKey.DatabasePath, string.Empty));
        }

        public async Task<bool> Save(DO.VehiclePolicy alert)
        {
            if (alert == null) return false;

            var mapped = Mapper.Map<DO.VehiclePolicy>(alert);       
            var result = await _connection.InsertAsync(mapped);

            if (result >= 1) return true;
            return false;
        }

        public async Task<IEnumerable<SO.VehiclePolicy>> Get()
        {
            var data = await _connection.Table<DO.VehiclePolicy>()                                   
                                   .ToListAsync();
            var mapped = Mapper.Map<IEnumerable<SO.VehiclePolicy>>(data);
            return mapped;
        }
        
    }
}

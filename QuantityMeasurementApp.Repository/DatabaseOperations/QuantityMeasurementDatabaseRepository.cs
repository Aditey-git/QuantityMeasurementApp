using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using QuantityAppModel;

namespace QuantityAppRepository
{
    public class QuantityMeasurementDatabaseRepository : IQuantityMeasurementRepository
    {
        private readonly QuantityMeasurementDbContext context;

        public QuantityMeasurementDatabaseRepository(QuantityMeasurementDbContext context)
        {
            this.context = context;   
        }

        public bool IsDatabaseAvailable()
        {
            return context.Database.CanConnect();
        }


        public void SaveMeasurement(QuantityMeasurementEntity entity)
        {
            context.QuantityMeasurements.Add(entity);
            context.SaveChangesAsync();
        }

        public List<QuantityMeasurementEntity> GetAllMeasurements()
        {
            return context.QuantityMeasurements.OrderByDescending(m => m.CreatedAt).ToList(); 
        }


        public List<QuantityMeasurementEntity> GetMeasurementsByOperation(string operationType)
        {
            return context.QuantityMeasurements.Where(m => m.OperationType == operationType).ToList();
        }


        public List<QuantityMeasurementEntity> GetMeasurementsByCategory(string category)
        {
            return context.QuantityMeasurements.Where(m => m.MeasurementCategory == category).ToList();
        }

        public int GetMeasurementCount()
        {
            return context.QuantityMeasurements.Count();
        }

        public void DeleteAllMeasurements()
        {
            context.QuantityMeasurements.RemoveRange(context.QuantityMeasurements);
            context.SaveChangesAsync();
        }

        
        public void SaveMeasurementsFromCache(List<QuantityMeasurementEntity> list)
        {
            foreach(var entity in list)
            {
                SaveMeasurement(entity);
            }
        }

    }
}
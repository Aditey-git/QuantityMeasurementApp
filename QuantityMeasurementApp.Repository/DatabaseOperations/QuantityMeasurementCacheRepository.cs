using System;
using System.Collections.Generic;
using System.Text.Json;
using QuantityAppModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace QuantityAppRepository 
{
    public class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
    {
        private readonly string cachePath = @"C:\Users\dell\Documents\web dev study\QualityMeasurementApp\QuantityMeasurementApp.Repository\DatabaseOperations\cache.json";
        private List<QuantityMeasurementEntity> measurements;

        public QuantityMeasurementCacheRepository()
        {
            LoadCache();
        }


        //Loads Cache from file when repository starts
        public void LoadCache()
        {
            if (File.Exists(cachePath))
            {
                string json = File.ReadAllText(cachePath);

                if (!string.IsNullOrWhiteSpace(json))
                {
                    measurements = JsonSerializer.Deserialize<List<QuantityMeasurementEntity>>(json) 
                                    ?? new List<QuantityMeasurementEntity>();
                }
                else
                {
                    measurements = new List<QuantityMeasurementEntity>();
                }
            }
            else
            {
                measurements = new List<QuantityMeasurementEntity>();
            }
        }


        public void SaveCache()
        {
            string json = JsonSerializer.Serialize(measurements, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(cachePath, json);
        }


        public void SaveMeasurement(QuantityMeasurementEntity entity)
        {
            entity.Id = measurements.Count + 1;

            measurements.Add(entity);

            SaveCache();
        }


        public List<QuantityMeasurementEntity> GetAllMeasurements()
        {
            return measurements.ToList();
        }

        public List<QuantityMeasurementEntity> GetMeasurementsByOperation(string operationType)
        {
            return measurements.Where(m => m.OperationType.Equals(operationType, StringComparison.OrdinalIgnoreCase)).ToList();
        }


        public List<QuantityMeasurementEntity> GetMeasurementsByCategory(string category)
        {
            return measurements.Where(m => m.MeasurementCategory.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public int GetMeasurementCount()
        {
            return measurements.Count;
        }

        public void DeleteAllMeasurements()
        {
            measurements.Clear();
            SaveCache();
        }

        public List<QuantityMeasurementEntity> GetAllCachedMeasurements()
        {
            if (!File.Exists(cachePath))
                return new List<QuantityMeasurementEntity>();

            string json = File.ReadAllText(cachePath);

            if (string.IsNullOrWhiteSpace(json))
                return new List<QuantityMeasurementEntity>();

            return JsonSerializer.Deserialize<List<QuantityMeasurementEntity>>(json)
                ?? new List<QuantityMeasurementEntity>();
        }


        public void ClearCache()
        {
            if (File.Exists(cachePath))
            {
                File.WriteAllText(cachePath, "[]");
            }
        }


    }   
}
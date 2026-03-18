using System.Collections.Generic;
using QuantityAppModel;

namespace QuantityAppRepository
{
    public interface IQuantityMeasurementRepository
    {
        // Save a measurement record into the database
        void SaveMeasurement(QuantityMeasurementEntity entity);

        // Retrieve all measurement records
        List<QuantityMeasurementEntity> GetAllMeasurements();

        // Retrieve measurements filtered by operation type
        List<QuantityMeasurementEntity> GetMeasurementsByOperation(string operationType);

        // Retrieve measurements filtered by category (Length, Weight, etc.)
        List<QuantityMeasurementEntity> GetMeasurementsByCategory(string category);

        // Get total number of stored measurements
        int GetMeasurementCount();

        // Delete all measurements (mainly useful for tests/reset)
        void DeleteAllMeasurements();
    }
}
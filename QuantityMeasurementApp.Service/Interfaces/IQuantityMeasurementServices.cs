using QuantityAppModel;

namespace QuantityAppService
{
    public interface IQuantityMeasurementService
    {
        MeasurementResponseDTO ProcessMeasurement(MeasurementRequestDTO request);

        List<QuantityMeasurementEntity> GetAllMeasurements();
        List<QuantityMeasurementEntity> GetMeasurementsByOperation(string operationType);
        List<QuantityMeasurementEntity> GetMeasurementsByCategory(string category);
        int GetMeasurementCount();
    }
}

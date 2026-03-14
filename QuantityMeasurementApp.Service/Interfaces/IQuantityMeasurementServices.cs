using QuantityAppModel;

namespace QuantityAppService
{
    public interface IQuantityMeasurementService
    {
        MeasurementResponseDTO ProcessMeasurement(MeasurementRequestDTO request);
    }
}

using QuantityAppModel;

namespace QuantityMeasurementApp
{
    public interface IMenu
    {
        MeasurementRequestDTO? DisplayAndGetRequest();
        void DisplayResult(MeasurementResponseDTO response);
    }
}

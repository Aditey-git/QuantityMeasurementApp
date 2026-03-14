namespace QuantityAppModel
{
    // DTO to carry measurement request data from Controller to Service layer
    public class MeasurementRequestDTO
    {
        public string MeasurementCategory { get; set; } = string.Empty;
        public MeasurementAction OperationType { get; set; }
        public string MeasurementUnit1 { get; set; } = string.Empty;
        public double MeasurementValue1 { get; set; }
        public string MeasurementUnit2 { get; set; } = string.Empty;
        public double MeasurementValue2 { get; set; }
        public string TargetMeasurementUnit { get; set; } = string.Empty;
    }
}

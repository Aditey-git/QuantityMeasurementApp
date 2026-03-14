namespace QuantityAppModel
{
    // DTO to carry measurement result data from Service layer back to Controller
    public class MeasurementResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public bool IsComparison { get; set; }
        public bool AreEqual { get; set; }
        public double CalculatedValue { get; set; }
        public string FormattedMessage { get; set; } = string.Empty;
    }
}

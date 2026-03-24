using System.ComponentModel.DataAnnotations;

namespace QuantityAppModel
{
    // DTO to carry measurement request data from Controller to Service layer
    public class MeasurementRequestDTO
    {
        [Required]
        public string MeasurementCategory { get; set; } = string.Empty;

        [Required]
        public MeasurementAction OperationType { get; set; }

        [Required]
        public string MeasurementUnit1 { get; set; } = string.Empty;

        [Required]
        public double MeasurementValue1 { get; set; }

        [Required]
        public string MeasurementUnit2 { get; set; } = string.Empty;

        [Required]
        public double MeasurementValue2 { get; set; }
        public string TargetMeasurementUnit { get; set; } = string.Empty;
    }
}

using System;

namespace QuantityAppModel
{
    // Represents a database record for a measurement operation
    public class QuantityMeasurementEntity
    {
        public int Id { get; set; }

        // First operand
        public double Operand1Value { get; set; }
        public string Operand1Unit { get; set; } = string.Empty;

        // Second operand (nullable if operation doesn't need it)
        public double? Operand2Value { get; set; }
        public string? Operand2Unit { get; set; }

        // Operation information
        public string MeasurementCategory { get; set; } = string.Empty;
        public string OperationType { get; set; } = string.Empty;

        // Result of operation
        public double? ResultValue { get; set; }
        public string? ResultUnit { get; set; }

        // Error message if operation failed
        public string? ErrorMessage { get; set; }

        // Timestamp
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuantityAppModel
{
    // Represents a database record for a measurement operation
    [Table("QuantityMeasurements")]
    public class QuantityMeasurementEntity
    {
        [Key]
        public int Id { get; set; }

        // First operand
        public double Operand1Value { get; set; }
        public string Operand1Unit { get; set; } = string.Empty;

        // Second operand (nullable if operation doesn't need it)
        public double? Operand2Value { get; set; }
        public string? Operand2Unit { get; set; }

        // Operation information
        [Column("MeasurementType")]
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
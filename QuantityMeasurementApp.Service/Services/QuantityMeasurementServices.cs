using System;
using QuantityAppModel;
using QuantityAppRepository;

namespace QuantityAppService
{
    // Orchestrates business logic and routes requests safely
    public class QuantityMeasurementServices : IQuantityMeasurementService
    {
        private readonly IQuantityMeasurementRepository repository;

        public QuantityMeasurementServices(IQuantityMeasurementRepository repo)
        {
            repository = repo;
        }


        // Validates floats
        public void ValidateValue(double checkValue)
        {
            if (double.IsNegative(checkValue) || double.IsInfinity(checkValue))
                throw new InvalidMeasurementException($"The measurement value {checkValue} is invalid.");
        }

        public MeasurementResponseDTO ProcessMeasurement(MeasurementRequestDTO request)
        {
            try
            {
                // Route to the correct generic method based on the category string
                return request.MeasurementCategory.ToLower() switch
                {
                    "length" => ProcessCategory<LengthUnit>(request, LengthConverter.Instance),
                    "volume" => ProcessCategory<VolumeUnit>(request, VolumeConverter.Instance),
                    "weight" => ProcessCategory<WeightUnit>(request, WeightConverter.Instance),
                    "temperature" => ProcessCategory<TemperatureUnit>(request, TemperatureConverter.Instance),
                    _ => throw new ArgumentException("Invalid category")
                };
            }
            catch (Exception ex)
            {
                var entity = new QuantityMeasurementEntity
                {
                    MeasurementCategory = request.MeasurementCategory,
                    OperationType = request.OperationType.ToString(),
                    Operand1Value = request.MeasurementValue1,
                    Operand1Unit = request.MeasurementUnit1,
                    Operand2Value = request.MeasurementValue2,
                    Operand2Unit = request.MeasurementUnit2,
                    ErrorMessage = ex.Message,
                    CreatedAt = DateTime.Now
                };

                repository.SaveMeasurement(entity);

                return new MeasurementResponseDTO { IsSuccess = false, ErrorMessage = ex.Message };
            }
        }

        // Parses generic boundaries and executes math
        private MeasurementResponseDTO ProcessCategory<TUnit>(MeasurementRequestDTO req, IMeasurable<TUnit> converter) where TUnit : struct, Enum
        {
            // Validating inputs
            if (typeof(TUnit) != typeof(TemperatureUnit)) 
            {
               ValidateValue(req.MeasurementValue1);
               if (req.OperationType != MeasurementAction.Compare)
               {
                   ValidateValue(req.MeasurementValue2);
               } 
            }

            if (!Enum.TryParse(req.MeasurementUnit1, true, out TUnit u1) || !Enum.TryParse(req.MeasurementUnit2, true, out TUnit u2))
                throw new ArgumentException("Invalid unit provided.");

            var q1 = new Quantity<TUnit>(req.MeasurementValue1, u1, converter);
            var q2 = new Quantity<TUnit>(req.MeasurementValue2, u2, converter);

            if (req.OperationType == MeasurementAction.Compare)
            {

                var entity = new QuantityMeasurementEntity
                {
                    MeasurementCategory = req.MeasurementCategory,
                    OperationType = req.OperationType.ToString(),
                    Operand1Value = req.MeasurementValue1,
                    Operand1Unit = req.MeasurementUnit1,
                    Operand2Value = req.MeasurementValue2,
                    Operand2Unit = req.MeasurementUnit2,
                    ResultValue = null,
                    ResultUnit = null,
                    CreatedAt = DateTime.Now
                };

                return new MeasurementResponseDTO
                {
                    IsSuccess = true,
                    IsComparison = true,
                    AreEqual = q1.Equals(q2)
                };
            }

            // If it's Arithmetic, parse the Target Unit
            if (!Enum.TryParse(req.TargetMeasurementUnit, true, out TUnit targetUnit))
            {
                throw new ArgumentException("Invalid target unit provided.");
            }

            if (typeof(TUnit) == typeof(TemperatureUnit))
                throw new InvalidMeasurementException("Arithmetic operations (Add/Subtract/Divide) are not supported for Temperature.");

            Quantity<TUnit> result = req.OperationType switch
            {
                MeasurementAction.Add => q1.Add(q2, targetUnit),
                MeasurementAction.Subtract => q1.Subtract(q2, targetUnit),
                MeasurementAction.Divide => q1.Division(q2, targetUnit),
                _ => throw new ArgumentException("Invalid operation")
            };

            string symbol = req.OperationType switch { MeasurementAction.Add => "+", MeasurementAction.Subtract => "-", _ => "/" };

            double calculatedValue = result.ConvertTo(targetUnit);

            var entityResult = new QuantityMeasurementEntity
            {
                MeasurementCategory = req.MeasurementCategory,
                OperationType = req.OperationType.ToString(),
                Operand1Value = req.MeasurementValue1,
                Operand1Unit = req.MeasurementUnit1,
                Operand2Value = req.MeasurementValue2,
                Operand2Unit = req.MeasurementUnit2,
                ResultValue = calculatedValue,
                ResultUnit = targetUnit.ToString(),
                CreatedAt = DateTime.Now
            };

            repository.SaveMeasurement(entityResult);


            return new MeasurementResponseDTO
            {
                IsSuccess = true,
                IsComparison = false,
                CalculatedValue = result.ConvertTo(targetUnit),
                FormattedMessage = $"{req.MeasurementValue1} {u1} {symbol} {req.MeasurementValue2} {u2} = {result.ConvertTo(targetUnit)} {targetUnit}"
            };
        }


        public List<QuantityMeasurementEntity> GetAllMeasurements()
        {
            return repository.GetAllMeasurements();
        }


        public List<QuantityMeasurementEntity> GetMeasurementsByOperation(string operationType)
        {
            return repository.GetMeasurementsByOperation(operationType);
        }

        public List<QuantityMeasurementEntity> GetMeasurementsByCategory(string category)
        {
            return repository.GetMeasurementsByCategory(category);
        }

        public int GetMeasurementCount()
        {
            return repository.GetMeasurementCount();
        }
    }
}

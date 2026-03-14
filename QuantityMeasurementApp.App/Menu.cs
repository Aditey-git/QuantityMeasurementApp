using System;
using QuantityAppModel;

namespace QuantityMeasurementApp
{
    public class Menu : IMenu
    {
        public MeasurementRequestDTO? DisplayAndGetRequest()
        {
            Console.WriteLine("\n--- Main Menu ---");
            Console.WriteLine("Type Of Units Available : \n1. Weight\n2. Length\n3. Volume\n4. Temperature\n5. Exit");
            Console.Write("Choice: ");

            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 5)
            {
                Console.WriteLine("Invalid choice.");
                return null;
            }

            if (choice == 5)
            {
                return new MeasurementRequestDTO { MeasurementCategory = "Exit" };
            }

            var request = new MeasurementRequestDTO();
            request.MeasurementCategory = choice switch { 1 => "Weight", 2 => "Length", 3 => "Volume", 4 => "Temperature", _ => "Unknown" };
            
            Console.WriteLine($"\n{request.MeasurementCategory} Operations");
            if (choice == 4)
            {
                Console.WriteLine("1. Compare");
                // Temperature ONLY supports Compare
            }
            else
            {
                Console.WriteLine("1. Compare\n2. Add\n3. Subtract\n4. Divide");
            }

            Console.Write("\nSelect Operation: ");
            if (!int.TryParse(Console.ReadLine(), out int opChoice))
            {
                Console.WriteLine("Invalid operation.");
                return null;
            }

            if (choice == 4 && opChoice != 1)
            {
                Console.WriteLine("Temperature only supports Comparison.");
                return null;
            }

            request.OperationType = (MeasurementAction)opChoice;

            string expectedUnits = choice switch
            {
                1 => "Kilogram, Gram, Pound",
                2 => "Feet, Inch, Yard, Centimeters",
                3 => "Litre, Millilitre, Gallon",
                4 => "Celsius, Fahrenheit, Kelvin",
                _ => ""
            };

            Console.Write($"Enter First Value: ");
            if (!double.TryParse(Console.ReadLine(), out double val1)) return null;
            request.MeasurementValue1 = val1;

            Console.Write($"Enter First Unit ({expectedUnits}): ");
            request.MeasurementUnit1 = Console.ReadLine()?.Trim() ?? "";

            Console.Write($"Enter Second Value: ");
            if (!double.TryParse(Console.ReadLine(), out double val2)) return null;
            request.MeasurementValue2 = val2;

            Console.Write($"Enter Second Unit ({expectedUnits}): ");
            request.MeasurementUnit2 = Console.ReadLine()?.Trim() ?? "";

            if (request.OperationType != MeasurementAction.Compare)
            {
                Console.Write($"Enter Target Unit ({expectedUnits}): ");
                request.TargetMeasurementUnit = Console.ReadLine()?.Trim() ?? "";
            }

            return request;
        }

        public void DisplayResult(MeasurementResponseDTO response)
        {
            if (!response.IsSuccess)
            {
                Console.WriteLine($"\nError: {response.ErrorMessage}");
            }
            else if (response.IsComparison)
            {
                Console.WriteLine($"\nEquality Result: {response.AreEqual}");
            }
            else
            {
                Console.WriteLine($"\nRESULT: {response.FormattedMessage}");
            }
        }
    }
}

using System;
using QuantityAppModel;
using QuantityAppService;

namespace QuantityAppMain
{
    public class Menu
    {
        public void Run()
        {
            QuantityMeasurementService service = new QuantityMeasurementService();

            try
            {
                Console.WriteLine("Select comparison type:");
                Console.WriteLine("1. Compare Feet");
                Console.WriteLine("2. Compare Inches");
                Console.Write("Enter choice: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 1)
                {
                    Console.Write("Enter first value in feet: ");
                    double value1 = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Enter second value in feet: ");
                    double value2 = Convert.ToDouble(Console.ReadLine());

                    Feet feet1 = new Feet(value1);
                    Feet feet2 = new Feet(value2);

                    bool result = service.CompareFeet(feet1, feet2);

                    Console.WriteLine($"Are the two feet values equal: {result}");
                }
                else if (choice == 2)
                {
                    Console.Write("Enter first value in inches: ");
                    double value1 = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Enter second value in inches: ");
                    double value2 = Convert.ToDouble(Console.ReadLine());

                    Inch inch1 = new Inch(value1);
                    Inch inch2 = new Inch(value2);

                    bool result = service.CompareInch(inch1, inch2);

                    Console.WriteLine($"Are the two inch values equal: {result}");
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input. Please enter numeric values only.");
            }
        }
    }
}
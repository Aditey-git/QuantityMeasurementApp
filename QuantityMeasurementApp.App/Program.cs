using System;
using QuantityAppService;

namespace QuantityMeasurementApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create the dependencies
            IQuantityMeasurementService appService = new QuantityMeasurementServices();
            IMenu menu = new Menu();
            
            // Inject them into the controller
            QuantityMeasurementController applicationController = new QuantityMeasurementController(appService, menu);
            
            // Start the application
            applicationController.InitializeApplication();
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}

using System;
using QuantityAppModel;
using QuantityAppService;

namespace QuantityMeasurementApp
{
    public class QuantityMeasurementController
    {
        private readonly IQuantityMeasurementService _service;
        private readonly IMenu _menu;

        // Dependency Injection via constructor
        public QuantityMeasurementController(IQuantityMeasurementService service, IMenu menu)
        {
            _service = service;
            _menu = menu;
        }

        public void InitializeApplication()
        {
            while (true)
            {
                // Get user input from the Menu
                var request = _menu.DisplayAndGetRequest();
                
                // Continue looping if input is invalid
                if (request == null) continue;

                // Stop loop if the user selected Exit
                if (request.MeasurementCategory == "Exit") break;

                // Ship the DTO to the Service Layer
                var response = _service.ProcessMeasurement(request);

                // Display the Result via the Menu
                _menu.DisplayResult(response);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using QuantityAppService;
using QuantityAppModel;
using QuantityAppRepository;
using System.Runtime.ExceptionServices;
using System.IO.Pipelines;


namespace QuantityMeasurementApi.Controllers
{
    [ApiController]
    [Route("api/quantities")]
    public class QuantityController : ControllerBase
    {
        private readonly IQuantityMeasurementService service;

        public QuantityController(IQuantityMeasurementService service)
        {
            this.service = service;
        }

        [HttpPost("compare")]
        public ActionResult<MeasurementResponseDTO> CompareMeasurements([FromBody] MeasurementRequestDTO request)
        {
            var result = service.ProcessMeasurement(request);

            return Ok(result);
        }
        

        [HttpPost("add")]
        public ActionResult<MeasurementResponseDTO> AddMeasurements([FromBody] MeasurementRequestDTO request)
        {
            var result = service.ProcessMeasurement(request);

            return Ok(result);
        }


        [HttpPost("subtract")]
        public ActionResult<MeasurementResponseDTO> SubtractMeasurements([FromBody] MeasurementRequestDTO request)
        {
            var result = service.ProcessMeasurement(request);

            return Ok(result);
        }

        [HttpPost("divide")]
        public ActionResult<MeasurementResponseDTO> DivideMeasurements([FromBody] MeasurementRequestDTO request)
        {
            var result = service.ProcessMeasurement(request);

            return Ok(result);
        }

        [HttpGet("history")]
        public ActionResult<List<QuantityMeasurementEntity>> GetAllMeasurements()
        {
            var result = service.GetAllMeasurements();

            return Ok(result);
        }

        [HttpGet("history/operation/{operationType}")]
        public ActionResult<List<QuantityMeasurementEntity>> GetMeasurementsByOperationType(string operationType)
        {
            var result = service.GetMeasurementsByOperation(operationType);

            return Ok(result);
        }

        [HttpGet("history/category/{category}")]
        public ActionResult<List<QuantityMeasurementEntity>> GetMeasurementsByCategory(string category)
        {
            var result = service.GetMeasurementsByCategory(category);

            return Ok(result);
        }

        [HttpGet("count")]
        public ActionResult<int> GetMeasurementCount()
        {
            var count = service.GetMeasurementCount();

            return Ok(count);
        }

    }
}
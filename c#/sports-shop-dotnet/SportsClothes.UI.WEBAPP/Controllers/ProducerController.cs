using SportsClothes.UI.WEBAPP.Models.Dto;
using SportsClothes.UI.WEBAPP.Models.FilterObjects;
using SportsClothes.UI.WEBAPP.Services;
using Microsoft.AspNetCore.Mvc;

namespace SportsClothes.UI.WEBAPP.Controllers
{
    [Route("api/producers")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IProducerService _producerService;

        public ProducerController(IProducerService producerService)
        {
            _producerService = producerService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_producerService.GetProducers());
        }

        [HttpPost("filter")]
        public IActionResult Filter([FromBody] ProducerFilter filter)
        {
            return Ok(_producerService.GetFilteredProducers(filter));
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(_producerService.GetProducer(id));
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ProducerDto newProducer)
        {
            var validationResult = _producerService.Validate(newProducer);

            if (!validationResult.IsSuccess)
                return BadRequest(validationResult.Message);

            return Ok(_producerService.CreateProducer(newProducer));
        }

        [HttpPut("update/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProducerDto updatedProducer)
        {
            return Ok(_producerService.UpdateProducer(id, updatedProducer));
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            return Ok(_producerService.DeleteProducer(id));
        }
    }
}

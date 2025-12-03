using SportsClothes.UI.WEBAPP.Models.Dto;
using SportsClothes.UI.WEBAPP.Models.FilterObjects;
using SportsClothes.UI.WEBAPP.Services;
using Microsoft.AspNetCore.Mvc;

namespace SportsClothes.UI.WEBAPP.Controllers
{
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productService.GetProducts());
        }

        [HttpPost("filter")]
        public IActionResult Filter([FromBody] ProductFilter filter)
        {
            return Ok(_productService.GetFilteredProducts(filter));
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(_productService.GetProduct(id));
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ProductDto newProduct)
        {
            var validationResult = _productService.Validate(newProduct);

            if (!validationResult.IsSuccess)
                return BadRequest(validationResult.Message);

            return Ok(_productService.CreateProduct(newProduct));
        }

        [HttpPut("update/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProductDto updatedProduct)
        {
            return Ok(_productService.UpdateProduct(id, updatedProduct));
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            return Ok(_productService.DeleteProduct(id));
        }

        [HttpGet("producers")]
        public IActionResult GetProducersData()
        {
            return Ok(_productService.GetProducersData());
        }
    }
}

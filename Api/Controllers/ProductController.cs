using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IBaseRepository<Product> _repository;
        public ProductController(IBaseRepository<Product> repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var result = await _repository.GetById(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByEventId([FromQuery]int eventId, [FromServices] IProductRepository productRepository)
        {
            var result = await productRepository.GetAllProductByEventIdWithFees(eventId);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}

using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IBaseRepository<Event> _eventRepository;

        public EventController(IBaseRepository<Event> repository)
        {
            _eventRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetAll()
        {
            var result = await _eventRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetById(int id)
        {
            var result = await _eventRepository.GetById(id);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("fee/{id}")]
        public async Task<ActionResult<ServiceFee>> GetServiceFeeByEventId([FromServices] IEventRepository specificRepository, int id)
        {
            var result = await specificRepository.GetCorrespondingServiceFeeById(id);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task CreateNewEvent(Event input)
        {
            await _eventRepository.Create(input);
        }
    }
}

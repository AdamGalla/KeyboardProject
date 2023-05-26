using KeyboardAPI.Data;
using KeyboardAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KeyboardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyboardsController : ControllerBase
    {
        private readonly IRepository<Keyboard> _repository;

        public KeyboardsController(IRepository<Keyboard> repository)
        {
            _repository = repository;
        }
        // GET: api/<KeyboardsController>
        [HttpGet]
        public IEnumerable<Keyboard> Get()
        {
            return _repository.GetAll();
        }

        // GET api/<KeyboardsController>/5
        [HttpGet("{id}")]
        public ActionResult<Keyboard> Get(int id)
        {
            var keyboard = _repository.Get(id);
            if (keyboard == null)
            {
                return NotFound();
            }
            return new OkObjectResult(keyboard);
        }

        // POST api/<KeyboardsController>
        [HttpPost]
        public IActionResult Post([FromBody] Keyboard keyboard)
        {
            return Ok(_repository.Add(keyboard));
        }

        // PUT api/<KeyboardsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Keyboard keyboard)
        {
            if (keyboard == null || keyboard.Id != id)
            {
                return BadRequest();
            }

            var newKeyboard = _repository.Get(id);

            if (newKeyboard == null)
            {
                return NotFound();
            }

            newKeyboard.Name = keyboard.Name;
            newKeyboard.Image = keyboard.Image;
            newKeyboard.ReservedBy = keyboard.ReservedBy;

            _repository.Edit(newKeyboard);
            return new NoContentResult();
        }

        // DELETE api/<KeyboardsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_repository.Get(id) == null)
            {
                return NotFound();
            }

            _repository.Remove(id);
            return new NoContentResult();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Unleash;
using UserAPI.Data;
using UserAPI.Models;

namespace UserAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IRepository<User> _userRepository;
    private readonly IUnleash _unleash;

    public UsersController(IRepository<User> userRepository, IUnleash unleash)
    {
        _userRepository = userRepository;
        _unleash = unleash;
    }

    // GET: api/<UsersController>
    [HttpGet]
    public ActionResult<IEnumerable<User>> Get()
    {
        return Ok(_userRepository.GetAll());
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
        return Ok(_userRepository.Get(id));
    }

    [HttpGet("logIn/{username}")]
    public ActionResult<User> SimpleLogIn(string username)
    {
        if (_unleash.IsEnabled("LogIn"))
        {
            // do new, flashy thing
            return Ok(_userRepository.LogIn(username));
        }
        else
        {
            // do old, boring stuff
            return StatusCode(501, "This feature is not implemented!");
        }
    }

    // POST api/<UsersController>
    [HttpPost]
    public ActionResult Post([FromBody] User user)
    {
        _userRepository.Add(user);
        return Ok();
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] User user)
    {
        user.Id = id;
        _userRepository.Edit(user);
        return Ok();
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _userRepository.Remove(id);
        return Ok();
    }
}

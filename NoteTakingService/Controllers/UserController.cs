using Microsoft.AspNetCore.Mvc;
using NoteTakingService.Models;
using NoteTakingService.Repositories;

namespace NoteTakingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAll()
    {
        return Ok(UserRepository.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<User> GetById(int id)
    {
        var user = UserRepository.GetById(id);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpGet("team/{teamId:int}")]
    public ActionResult<IEnumerable<User>> GetByTeamId(int teamId)
    {
        return Ok(UserRepository.GetByTeamId(teamId));
    }

    [HttpPost]
    public ActionResult<User> Create([FromBody] User user)
    {
        if (user is null)
        {
            return BadRequest();
        }

        if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName) || string.IsNullOrWhiteSpace(user.Email))
        {
            return BadRequest("First name, last name, and email are required.");
        }

        var created = UserRepository.Create(user);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public ActionResult<User> Update(int id, [FromBody] User user)
    {
        if (user is null)
        {
            return BadRequest();
        }

        if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName) || string.IsNullOrWhiteSpace(user.Email))
        {
            return BadRequest("First name, last name, and email are required.");
        }

        var updated = UserRepository.Update(id, user);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        return UserRepository.Delete(id) ? NoContent() : NotFound();
    }
}

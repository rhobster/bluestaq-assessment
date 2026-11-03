using Microsoft.AspNetCore.Mvc;
using NoteTakingService.Models;
using NoteTakingService.Repositories;

namespace NoteTakingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Team>> GetAll()
    {
        return Ok(TeamRepository.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Team> GetById(int id)
    {
        var team = TeamRepository.GetById(id);
        return team is null ? NotFound() : Ok(team);
    }

    [HttpPost]
    public ActionResult<Team> Create([FromBody] Team team)
    {
        if (team is null)
        {
            return BadRequest();
        }

        if (string.IsNullOrWhiteSpace(team.Name))
        {
            return BadRequest("Team name is required.");
        }

        var created = TeamRepository.Create(team);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Team> Update(int id, [FromBody] Team team)
    {
        if (team is null)
        {
            return BadRequest();
        }

        if (string.IsNullOrWhiteSpace(team.Name))
        {
            return BadRequest("Team name is required.");
        }

        var updated = TeamRepository.Update(id, team);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        return TeamRepository.Delete(id) ? NoContent() : NotFound();
    }
}

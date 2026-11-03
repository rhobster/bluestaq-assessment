using Microsoft.AspNetCore.Mvc;
using NoteTakingService.Models;
using NoteTakingService.Repositories;

namespace NoteTakingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoteController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Note>> GetAll()
    {
        return Ok(NoteRepository.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Note> GetById(int id)
    {
        var note = NoteRepository.GetById(id);
        return note is null ? NotFound() : Ok(note);
    }

    [HttpGet("user/{userId:int}")]
    public ActionResult<IEnumerable<Note>> GetByUserId(int userId)
    {
        return Ok(NoteRepository.GetByUserId(userId));
    }

    [HttpPost]
    public ActionResult<Note> Create([FromBody] Note note)
    {
        if (note is null)
        {
            return BadRequest();
        }

        if (string.IsNullOrWhiteSpace(note.Description))
        {
            return BadRequest("Note description is required.");
        }

        var created = NoteRepository.Create(note);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Note> Update(int id, [FromBody] Note note)
    {
        if (note is null)
        {
            return BadRequest();
        }

        if (string.IsNullOrWhiteSpace(note.Description))
        {
            return BadRequest("Note description is required.");
        }

        var updated = NoteRepository.Update(id, note);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        return NoteRepository.Delete(id) ? NoContent() : NotFound();
    }
}

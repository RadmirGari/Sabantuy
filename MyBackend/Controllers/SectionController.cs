using Data.DBContext;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class SectionController : ControllerBase
{
    private const int CREATE_NEW = -1;
    private readonly MyDbContext   _db;
    private readonly IImageService _images;

    public SectionController(MyDbContext db, IImageService images)
    {
        _db     = db;
        _images = images;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Section>>> GetAll()
        => Ok(await _db.sections.Include(s => s.Images).ToListAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Section>> Get(int id)
    {
        var sec = await _db.sections
            .Include(s => s.Images)
            .FirstOrDefaultAsync(s => s.Id == id);

        return sec is null ? NotFound() : Ok(sec);
    }

    // upsert section + cascade image metadata changes via service
    [HttpPut("{id}/{password}")]
    public async Task<IActionResult> Upsert(int id, [FromBody] Section updated, string password)
    {
        // password check
        var envPwd = Environment.GetEnvironmentVariable("SECTION_ADMIN_PASSWORD");
        if (password != envPwd) 
            return Unauthorized();

        // create new
        if (id == CREATE_NEW)
        {
            _db.sections.Add(updated);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = updated.Id }, updated);
        }

        // update existing
        if (id != updated.Id) 
            return BadRequest();

        var existing = await _db.sections
            .Include(s => s.Images)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (existing == null) 
            return NotFound();

        existing.Name        = updated.Name;
        existing.Information = updated.Information;

        // find/removals
        var toRemove = existing.Images
            .Where(ei => !updated.Images.Any(ui => ui.Id == ei.Id))
            .ToList();
        foreach (var img in toRemove)
        {
            // call service to delete both file & DB row
            await _images.DeleteAsync(img.Id, password);
        }

        // add/update
        foreach (var ui in updated.Images)
        {
            if (ui.Id == 0)
                existing.Images.Add(ui);
            else
            {
                var ei = existing.Images.First(i => i.Id == ui.Id);
                ei.Url         = ui.Url;
                ei.FileName    = ui.FileName;
                ei.ContentType = ui.ContentType;
            }
        }

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("check/{password}")]
    public IActionResult CheckPassword(string password)
    {
        var envPwd = Environment.GetEnvironmentVariable("SECTION_ADMIN_PASSWORD");
        if (string.IsNullOrEmpty(envPwd) || password != envPwd)
            return Unauthorized("Invalid password.");
        return Ok("Password is valid.");
    }
}

using Data.DBContext;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class SectionController : ControllerBase {
    private readonly MyDbContext _context;
    private int DEFAULT_CREATE_NEW = -1;

    public SectionController(MyDbContext context){
        _context = context;
    }

    // Endpoint: GET /api/section
    // Read all sections.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Section>>> GetAllSections(){
        return await _context.sections.ToListAsync();
    }

    // Endpoint: Get /api/section/{id}
    // Read specific section
    [HttpGet("{id}")]
    public async Task<ActionResult<Section>> GetSectionById(int id){
        var section = await _context.sections.FindAsync(id);

        if (section == null)
            return NotFound();

        return section;
    }

    // PUT /api/sections/{id}/{password}
    // Creates a new Section if id == -1, otherwise updates the existing one.
    [HttpPut("{id}/{password}")]
    public async Task<IActionResult> UpdateSection(int id, [FromBody] Section updated, string password)
    {
        //Check password
        var envPwd = Environment.GetEnvironmentVariable("SECTION_ADMIN_PASSWORD");
        if (string.IsNullOrEmpty(envPwd) || password != envPwd)
            return Unauthorized("Invalid password.");

        // Create new
        if (id == DEFAULT_CREATE_NEW)
        {
            _context.sections.Add(updated);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSectionById), new { id = updated.Id }, updated);
        }

        // Update existing
        if (id != updated.Id)
            return BadRequest("ID in URL and body must match.");

        _context.Entry(updated).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.sections.AnyAsync(s => s.Id == id))
                return NotFound();
            throw;
        }

        return NoContent();
    }
}
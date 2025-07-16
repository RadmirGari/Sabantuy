using Data.DBContext;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/text")]
[ApiController]
public class TextBlockController : ControllerBase
{
    private readonly MyDbContext _db;
    private const int CREATE_NEW = -1;

    public TextBlockController(MyDbContext db)
    {
        _db = db;
    }

    [HttpGet("{label}")]
    public async Task<ActionResult<TextBlock>> GetByLabel(string label)
    {
        var block = await _db.textBlocks.FirstOrDefaultAsync(b => b.Label == label);
        return block is null ? NotFound() : Ok(block);
    }

    [HttpPut("{label}")]
    public async Task<IActionResult> Upsert(string label, [FromBody] string content)
    {
        var block = await _db.textBlocks.FirstOrDefaultAsync(b => b.Label == label);

        if (block == null)
        {
            block = new TextBlock { Label = label, Content = content };
            _db.textBlocks.Add(block);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByLabel), new { label = block.Label }, block);
        }

        block.Content = content;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{label}")]
    public async Task<IActionResult> Delete(string label)
    {
        var block = await _db.textBlocks.FirstOrDefaultAsync(b => b.Label == label);
        if (block == null) return NotFound();

        _db.textBlocks.Remove(block);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}

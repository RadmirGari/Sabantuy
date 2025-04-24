using Data.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/image")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IImageService _svc;

    public ImageController(IImageService svc) => _svc = svc;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Image>>> GetAll()
        => Ok(await _svc.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Image>> Get(int id)
    {
        var img = await _svc.GetByIdAsync(id);
        return img is null ? NotFound() : Ok(img);
    }

    [HttpPut("{id}/{password}")]
    public async Task<IActionResult> Upsert(int id, [FromBody] Image image, string password)
    {
        try
        {
            var result = await _svc.UpsertAsync(image, password);
            if (image.Id == -1)
                return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}/{password}")]
    public async Task<IActionResult> Delete(int id, string password)
    {
        try
        {
            await _svc.DeleteAsync(id, password);
            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}

using Data.DBContext;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class SubscriberController : ControllerBase {
    private readonly MyDbContext _context;
    private int DEFAULT_CREATE_NEW = -1;

    public SubscriberController(MyDbContext context){
        _context = context;
    }


    // Get /api/subscriber
    // Reads all subscribers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Subscribers>>> getAllSubscribers(){
        return await _context.subscribers.ToListAsync();
    }

    // GET /api/subscriber/{id}
    // Reads a specific subscriber
    [HttpGet("{id}")]
    public async Task<ActionResult<Subscribers>> GetSubscriberById(int id)
    {
        var sub = await _context.subscribers.FindAsync(id);
        if (sub == null) return NotFound();
        return sub;
    }

    // Put /api/subscriber
    // Puts a new subscriber
   [HttpPut]
    public async Task<ActionResult<Subscribers>> AddSubscriber([FromBody] Subscribers subscriber)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            _context.subscribers.Add(subscriber);
            await _context.SaveChangesAsync();

            // Returns 201 Created with a Location header pointing at GET /api/subscriber/{id}
            return CreatedAtAction(
                nameof(GetSubscriberById), 
                new { id = subscriber.Id }, 
                subscriber
            );
        }
        catch (DbUpdateException dbEx)
        {
            Console.WriteLine("NOTE!!! Exception Occured: ", dbEx);
            return BadRequest("Could not add subscriber.");
        }
    }
}
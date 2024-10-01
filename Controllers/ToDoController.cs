using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Data;
using ToDoListAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDoController(ToDoContext context)
        {
            _context = context;
        }

        // GET: api/todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            return await _context.ToDoItems
                .Where(t => t.CompletedDate == null)
                .ToListAsync();
        }

        // GET: api/todo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(int id)
        {
            var item = await _context.ToDoItems.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST: api/todo
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> PostToDoItem(ToDoItem item)
        {
            _context.ToDoItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetToDoItem), new { id = item.Id }, item);
        }

        // PUT: api/todo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoItem(int id, ToDoItem item)
        {
            var existingItem = await _context.ToDoItems.FindAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.CompletedDate = System.DateTime.UtcNow;
            _context.Entry(existingItem).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/todo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(int id)
        {
            var item = await _context.ToDoItems.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.ToDoItems.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
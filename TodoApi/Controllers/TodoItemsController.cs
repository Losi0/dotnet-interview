using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Dtos;
using TodoApi.Models;
namespace TodoApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/todolist/{listId}/items")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateItem(long listId, [FromBody] string description)
        {
            var list = await _context.TodoList.FindAsync(listId);
            if (list == null) return NotFound();

            var item = new TodoItem { Description = description, IsComplete = false, ListId = listId };
            _context.TodoItem.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItem), new { listId = listId, id = item.ListId }, item);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetItem(long listId, long id)
        {
            var item = await _context.TodoItem.FindAsync(id);
            if (item == null || item.ListId != listId) return NotFound();

            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(long listId, long id, [FromBody] string newDescription)
        {
            var item = await _context.TodoItem.FindAsync(id);
            if (item == null || item.ListId != listId) return NotFound();

            item.Description = newDescription;
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> CompleteItem(long listId, long id)
        {
            var item = await _context.TodoItem.FindAsync(id);
            if (item == null || item.ListId != listId) return NotFound();

            item.IsComplete = true;
            await _context.SaveChangesAsync();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(long listId, long id)
        {
            var item = await _context.TodoItem.FindAsync(id);
            if (item == null || item.ListId != listId) return NotFound();

            _context.TodoItem.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

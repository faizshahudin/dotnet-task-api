using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using System.Runtime.Intrinsics.X86;

namespace TaskManagementAPI.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase {
        private readonly TaskDbContext _context;

        public TasksController(TaskDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<models.Task>> GetTasks() {
            return await _context.Tasks.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<models.Task>> GetTask(int id) {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<models.Task>> CreateTask(models.Task task) {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(int id, models.Task updatedTask) {
            if (id != updatedTask.Id) {
                return BadRequest();
            }

            _context.Entry(updatedTask).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!TaskExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id) {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id) {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
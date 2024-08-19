using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.models;
using System.Collections.Generic;

namespace TaskManagementAPI.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase {
        private static readonly List<models.Task> tasks = [];

        [HttpGet]
        public ActionResult<IEnumerable<models.Task>> GetTasks() {
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<models.Task> GetTask(int id) {
            var task = tasks.Find(t => t.Id == id);
            if (task == null) {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public ActionResult<models.Task> CreateTask(models.Task task) {
            task.Id = tasks.Count + 1;
            tasks.Add(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTask(int id, models.Task updatedTask) {
            var task = tasks.Find(t => t.Id == id);
            if (task == null) {
                return NotFound();
            }

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.IsCompleted = updatedTask.IsCompleted;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id) {
            var task = tasks.Find(t => t.Id == id);
            if (task == null) {
                return NotFound();
            }

            tasks.Remove(task);
            return NoContent();
        }
    }
}
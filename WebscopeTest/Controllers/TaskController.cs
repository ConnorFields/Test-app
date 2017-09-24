using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebscopeTest.Models;
using System.Linq;

namespace WebscopeTest.Controllers
{
    [Route("Task/[TaskController]")]
    public class TaskController : Controller
    {
        private readonly TaskContext _context;

        public TaskController(TaskContext context)
        {
            _context = context;

            if (_context.TaskItems.Count() == 0)
            {
                _context.TaskItems.Add(new TaskItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<TaskItem> GetAll()
        {
            return _context.TaskItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTask")]
        public IActionResult GetById(long id)
        {
            var item = _context.TaskItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TaskItem item)
        {
            if (item == null) return BadRequest();

            _context.TaskItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTask", new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TaskItems.FirstOrDefault(x => x.Id == id);

            if (todo == null) return NotFound();

            _context.TaskItems.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}

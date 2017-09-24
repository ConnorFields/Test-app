using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebscopeTest.Models;
using System.Linq;

namespace WebscopeTest.Controllers
{
    [Route("task/[TaskController]")]
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
            var task = _context.TaskItems.FirstOrDefault(x => x.Id == id);

            if (task == null) return NotFound();

            _context.TaskItems.Remove(task);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult TimeOut(long id)
        {
            var task = _context.TaskItems.FirstOrDefault(x => x.Id == id);

            if (task == null) return NotFound();

            _context.TaskItems.Remove(task);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}

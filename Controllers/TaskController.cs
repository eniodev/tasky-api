using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tarefasbackend.Models;

namespace tarefasbackend.Controllers
{
    [Authorize]
    [Route("task")]
    [ApiController]
    public class TaskController: Controller
    {
        private readonly ITaskRepository _taskRepository;
      public TaskController(ITaskRepository taskRepository)
      {
        _taskRepository = taskRepository;
      }

     [HttpGet]
      public IActionResult GetTasks()
      {
        var id = new Guid(User.Identity.Name);
        var tasks = _taskRepository.Read(id);
        return Ok(tasks);
      }

      [HttpPost]
      public IActionResult Create([FromBody] Tasks model)
      {
        if(!ModelState.IsValid)
            return BadRequest();

        model.UserId = new Guid(User.Identity.Name);

        _taskRepository.Create(model);

        return Ok();
      }

      [HttpPut("{id}")]
      public IActionResult Update(string id, [FromBody] Tasks model)
      { 
        
        if(!ModelState.IsValid)
            return BadRequest();

        _taskRepository.Update(new System.Guid(id), model);

        return Ok();
      }

      [HttpDelete("{id}")]
      public IActionResult Update(string id)
      { 
        _taskRepository.Delete(new System.Guid(id));

        return Ok();
      }

    }
}

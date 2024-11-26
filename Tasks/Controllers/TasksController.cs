using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Models;

namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private static List<ModelTask> modelTasks = 
                new List<ModelTask>();


        [HttpGet]
        public ActionResult<List<ModelTask>> SearchTasks()
        {
            return Ok(modelTasks);
        }

        [HttpPost]
        public ActionResult<List<ModelTask>> AddTask(ModelTask newTask)
        {
            if (newTask.Description.Length < 10)
                return BadRequest("Need more characters");

            newTask.Id = modelTasks.Count > 0
                                ? modelTasks[modelTasks.Count - 1].Id + 1
                                : 1;

            modelTasks.Add(newTask);
            return Ok(modelTasks);
        }

        [HttpDelete("{id}")]
        public ActionResult RemoveTask(int id)
        {
            var finded = modelTasks.Find(x => x.Id == id);

            if (finded is null)
                return NotFound("This task doesn't exist");

            modelTasks.Remove(finded);
            return Ok(); 
        }

    }
}

using APIProjecteKanban.DAL.Model;
using APIProjecteKanban.DAL.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace APIProjecteKanban.Controllers
{
    [EnableCors]
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        // GET tasks/5
        [HttpGet("{idProject}")]
        public List<DAL.Model.Task> Get(int idProject)
        {
            TaskService objTaskService = new();
            return objTaskService.GetAllTasksFromProjectId(idProject);
        }
        // POST tasks/5
        [HttpPost("{idProject}")]
        public DAL.Model.Task Post(int idProject, [FromBody] DAL.Model.Task task)
        {
            TaskService objTaskService = new();
            return objTaskService.Add(idProject, task);
        }
        // PUT tasks/5
        [HttpPut("{idProject}")]
        public int Put(int idProject, [FromBody] DAL.Model.Task task)
        {
            TaskService objTaskService = new();
            return objTaskService.Update(idProject, task);
        }
    }
}

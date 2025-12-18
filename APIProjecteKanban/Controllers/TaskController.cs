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
        [HttpGet("{idProject}")]
        public List<DAL.Model.Task> Get(int idProject)
        {
            TaskService objTaskService = new();
            return objTaskService.GetAllTasksFromProjectId(idProject);
        }

        [HttpPost("{idProject}")]
        public DAL.Model.Task Post(int idProject, [FromBody] DAL.Model.Task task)
        {
            TaskService objTaskService = new TaskService();
            return objTaskService.Add(idProject, task);
        }

        [HttpPut("{idProject}")]
        public int Put(int idProject, [FromBody] DAL.Model.Task task)
        {
            TaskService objTaskService = new TaskService();
            return objTaskService.Update(idProject, task);
        }
    }
}

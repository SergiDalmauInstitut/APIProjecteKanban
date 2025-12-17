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
        [HttpGet("{id}")]
        public List<DAL.Model.Task> Get(int id)
        {
            TaskService objTaskService = new();
            return objTaskService.GetAllTasksFromProjectId(id);
        }
    }
}

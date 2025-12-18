using APIProjecteKanban.DAL.Model;
using APIProjecteKanban.DAL.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace APIProjecteKanban.Controllers
{
    [EnableCors]
    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        // GET projects/users/5
        [HttpGet("users/{IdUser}")]
        public List<Project> GetProjectsFromUserID(int IdUser)
        {
            ProjectService objProjectService = new();
            return objProjectService.GetAllProjectsFromUserId(IdUser);
        }
        // GET projects/5
        [HttpGet("{IdProject}")]
        public Project? GetProjectFromId(int IdProject)
        {
            ProjectService objProjectService = new();
            return objProjectService.GetProjectFromId(IdProject);
        }
    }
}

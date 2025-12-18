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
        // GET projects/5
        [HttpGet("users/{IdUser}")]
        public List<Project> GetProjectsFromUserID(int IdUser)
        {
            ProjectService objProjectService = new ProjectService();
            return objProjectService.GetAllProjectsFromUserId(IdUser);
        }

        [HttpGet("{IdProject}")]
        public Project? GetProjectFromId(int IdProject)
        {
            ProjectService objProjectService = new ProjectService();
            return objProjectService.GetProjectFromId(IdProject);
        }
    }
}

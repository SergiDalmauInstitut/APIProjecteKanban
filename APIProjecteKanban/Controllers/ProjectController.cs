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
        // GET users/5
        [HttpGet("{id}")]
        public List<Project> Get(int id)
        {
            ProjectService objProjectService = new ProjectService();
            return objProjectService.GetAllFromUserId(id);
        }
    }
}

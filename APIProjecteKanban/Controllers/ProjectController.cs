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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateProject([FromBody] Project newProject)
        {
            if (newProject == null || newProject.IdOwner == 0)
            {
                return BadRequest("Les dades del projecte o l'IdOwner no són vàlides.");
            }

            ProjectService objProjectService = new();

            try
            {
                Project createdProject = objProjectService.Add(newProject);

                if (createdProject.Id > 0)
                {
                    long projectId = createdProject.Id;
                    long ownerId = createdProject.IdOwner;

                    int rowsAffected = objProjectService.AddUserToProject(ownerId, projectId);

                    return CreatedAtAction(nameof(GetProjectFromId), new { IdProject = createdProject.Id }, createdProject);
                }

                return BadRequest("No s'ha pogut crear el projecte.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error en crear el projecte: {ex.Message}");
            }
        }


        [HttpPost("{IdProject}/users/{IdUser}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddUserToExistingProject(long IdProject, long IdUser)
        {
            ProjectService objProjectService = new();

            try
            {
                int rowsAffected = objProjectService.AddUserToProject(IdUser, IdProject);

                if (rowsAffected > 0)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound($"No s'ha pogut afegir l'usuari {IdUser} al projecte {IdProject}.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error en afegir l'usuari al projecte: {ex.Message}");
            }
        }

        // DELETE users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ProjectService objProjectService = new();
            objProjectService.Delete(id);
        }
    }
}
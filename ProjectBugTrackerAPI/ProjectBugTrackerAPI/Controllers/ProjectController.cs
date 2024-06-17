using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Mappers;
using ProjectBugTrackerAPI.Repository.Interface;

namespace ProjectBugTrackerAPI.Controllers
{
    [Route("api/Project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger _logger;

        public ProjectController(IProjectRepository projectRepository, ILogger<ProjectController> logger)
        {
            _projectRepository = projectRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProjects()
        {
            try
            {
                var result = await _projectRepository.GetAllProjects();
                var projects = result.Select(c => c.ToProjectDto()).ToList();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetProjects)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProject([FromRoute] int id)
        {
            try
            {
                var project = await _projectRepository.GetProjectById(id);
                if (project == null)
                {
                    return NotFound("Project Not Fount");
                }
                return Ok(project.ToProjectDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetProject)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProject([FromBody] CreateUpdateProjectDto projectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var project = projectDto.AsProjectModel();
                var result = await _projectRepository.AddProject(project);
                return CreatedAtAction(nameof(GetProject), new { id = result.Id }, result.ToProjectDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateProject)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProject([FromRoute] int id, CreateUpdateProjectDto projectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _projectRepository.UpdateProject(id, projectDto);

                if (result == null)
                {
                    return NotFound("Project Not Found");
                }
                return Ok(result.ToProjectDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateProject)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProject([FromRoute] int id)
        {
            try
            {
                bool result = await _projectRepository.DeleteProject(id);

                if (!result)
                {
                    return NotFound("Project Not Found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteProject)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }
    }
}

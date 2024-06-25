using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Mappers;
using ProjectBugTrackerAPI.Repository.Interface;

namespace ProjectBugTrackerAPI.Controllers
{
    [Route("api/clientrep")]
    [ApiController]
    public class ClientRepController : ControllerBase
    {
        private readonly IClientRepRepository _clientRepRepository;
        private readonly ILogger _logger;

        public ClientRepController(IClientRepRepository clientRepRepository, ILogger<ClientRepController> logger)
        {
            _clientRepRepository = clientRepRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ClientRepDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClientReps()
        {
            try
            {
                var result = await _clientRepRepository.GetAllClientReps();
                var clientReps = result.Select(c => c.ToClientRepDto()).ToList();
                return Ok(clientReps);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetClientReps)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ClientRepDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClientRep([FromRoute] int id)
        {
            try
            {
                var clientRep = await _clientRepRepository.GetClientRepById(id);
                if (clientRep == null)
                {
                    return NotFound("ClientRep Not Fount");
                }
                return Ok(clientRep.ToClientRepDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetClientRep)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClientRepDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateClientRep([FromBody] CreateUpdateClientRepDto clientRepDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var email = await _clientRepRepository.CheckClientRepEmail(clientRepDto.Email);
                if (email != null)
                {
                    ModelState.AddModelError("Code", "Email already in use");
                    return BadRequest(ModelState);
                }
                var clientRep = clientRepDto.AsClientRepModel();
                var result = await _clientRepRepository.AddClientRep(clientRep);
                return CreatedAtAction(nameof(GetClientRep), new { id = result.Id }, result.ToClientRepDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateClientRep)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ClientRepDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateClientRep([FromRoute] int id, CreateUpdateClientRepDto clientRepDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var email = await _clientRepRepository.CheckClientRepEmail(clientRepDto.Email);
                if (email != null && email.Id != id)
                {
                    ModelState.AddModelError("Email", "Email already in use");
                    return BadRequest(ModelState);
                }
                var result = await _clientRepRepository.UpdateClientRep(id, clientRepDto);

                if (result == null)
                {
                    return NotFound("ClientRep Not Found");
                }
                return Ok(result.ToClientRepDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateClientRep)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteClientRep([FromRoute] int id)
        {
            try
            {
                bool result = await _clientRepRepository.DeleteClientRep(id);

                if (!result)
                {
                    return NotFound("ClientRep Not Found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteClientRep)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }
    }
}

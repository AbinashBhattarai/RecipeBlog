using Microsoft.AspNetCore.Mvc;
using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Mappers;
using ProjectBugTrackerAPI.Repository.Interface;

namespace ProjectBugTrackerAPI.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger _logger;

        public ClientController(IClientRepository clientRepository, ILogger<ClientController> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClients()
        {
            try
            {
                var result = await _clientRepository.GetAllClients();
                var companies = result.Select(c => c.ToClientDto())
                                .ToList();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetClients)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClient([FromRoute] int id)
        {
            try
            {
                var client = await _clientRepository.GetClientById(id);
                if (client == null)
                {
                    return NotFound("Client Not Found");
                }
                return Ok(client.ToClientDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetClient)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDto clientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var code = await _clientRepository.GetClientByCode(clientDto.Code);
                if (code != null)
                {
                    ModelState.AddModelError("Code", "Client code already in use");
                    return BadRequest(ModelState);
                }
                var client = clientDto.AsClientModel();
                var result = await _clientRepository.AddClient(client);
                return CreatedAtAction(nameof(GetClient), new { id = result.Id }, result.ToClientDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateClient)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateClient([FromRoute] int id, [FromBody] UpdateClientDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _clientRepository.UpdateClient(id, dto);

                if (result == null)
                {
                    return NotFound("Client Not Found");
                }
                return Ok(result.ToClientDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateClient)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
        {
            try
            {
                bool result = await _clientRepository.DeleteClient(id);

                if (!result)
                {
                    return NotFound("Client Not Found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteClient)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }
    }
}

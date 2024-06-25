using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Models;

namespace ProjectBugTrackerAPI.Mappers
{
    public static class ClientMapper
    {
        public static ClientDto ToClientDto(this Client client)
        {
            return new ClientDto
            {
                Id = client.Id,
                Code = client.Code,
                Details = client.Details,
            };
        }

        public static Client AsClientModel(this CreateClientDto dto)
        {
            return new Client
            {
                Code = dto.Code,
                Details = dto.Details
            };
        }
    }
}

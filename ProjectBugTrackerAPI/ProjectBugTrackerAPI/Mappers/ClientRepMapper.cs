using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Models;

namespace ProjectBugTrackerAPI.Mappers
{
    public static class ClientRepMapper
    {
        public static ClientRepDto ToClientRepDto(this ClientRep clientRep)
        {
            return new ClientRepDto
            {
                Id = clientRep.Id,
                Name = clientRep.Name,
                Email = clientRep.Email,
                Phone = clientRep.Phone,
                Address = clientRep.Address,
            };
        }

        public static ClientRep AsClientRepModel(this CreateUpdateClientRepDto dto)
        {
            return new ClientRep
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
            };
        }
    }
}

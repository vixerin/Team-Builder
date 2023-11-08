using TeamBuilder.Common.Functional;
using TeamBuilder.DTO.Team.Infrastructure;
using TeamBuilder.TeamMembers.Application.Enums;
using TeamBuilder.TeamMembers.Application.Models;
using TeamBuilder.TeamMembers.Domain;
using TeamBuilder.TeamMembers.Infrastructure.Base;

namespace TeamBuilder.TeamMembers.Infrastructure
{
    public class TeamMembersRepository : BaseRepository, ITeamMembersRepository
    {
        public async Task<Result<List<TeamMemberViewModel>>> GetTeamMembers(DisplayMode displayMode)
        {
            bool? isActive = displayMode != DisplayMode.Inactive;

            string endpointUri = $"{Guid.NewGuid()}/{isActive}/Members";
            if(displayMode == DisplayMode.All)
                endpointUri = $"{Guid.NewGuid()}/Members";

            var teamMembersResult = await GetAsync<List<TeamMemberDto>>(endpointUri);

            if (teamMembersResult.IsFailure)
                return Result.Fail<List<TeamMemberViewModel>>(teamMembersResult.Error);

            var teamMemberViewModels = new List<TeamMemberViewModel>();
            teamMembersResult.Value.ForEach(x => teamMemberViewModels.Add(x.ToViewModel()));

            return Result.Ok(teamMemberViewModels);
        }

        public async Task<Result> AddTeamMembers(List<TeamMemberViewModel> teamMemberViewModels)
        {
            string endpointUri = $"{Guid.NewGuid()}/AddMembers";

            var teamMemberDtos = new List<TeamMemberDto>();
            teamMemberViewModels.ForEach(x => teamMemberDtos.Add(x.ToDto()));

            var addTeamMemberResult = await PostAsync(endpointUri, teamMemberDtos);

            return addTeamMemberResult;
        }
    }
}


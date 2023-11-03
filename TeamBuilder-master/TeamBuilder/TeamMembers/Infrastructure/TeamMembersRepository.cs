using TeamBuilder.DTO.Team.Infrastructure;
using TeamBuilder.Functional;
using TeamBuilder.TeamMembers.Application.Models;
using TeamBuilder.TeamMembers.Domain;

namespace TeamBuilder.TeamMembers.Infrastructure
{
    public class TeamMembersRepository : BaseRepository, ITeamMembersRepository
    {
        public async  Task<Result<List<TeamMemberViewModel>>> GetTeamMembers()
        {
            string endpointUri = $"{Guid.NewGuid()}/Members";
            var teamMembersResult = await GetAsync<List<TeamMemberDto>>(endpointUri);

            if (teamMembersResult.IsFailure)
                return Result.Fail<List<TeamMemberViewModel>>(teamMembersResult.Error);

            var teamMemberViewModels = new List<TeamMemberViewModel>();
            teamMembersResult.Value.ForEach(x => teamMemberViewModels.Add(x.ToViewModel()));

            return Result.Ok(teamMemberViewModels);
        }

        public async Task<Result> AddTeamMember(TeamMemberViewModel teamMemberViewModel)
        {
            string endpointUri = $"{Guid.NewGuid()}/AddMember";
            var addTeamMemberResult = await PostAsync(endpointUri, teamMemberViewModel.ToDto());

            return addTeamMemberResult.IsFailure ? Result.Fail(addTeamMemberResult.Error) : Result.Ok();
        }
    }
}


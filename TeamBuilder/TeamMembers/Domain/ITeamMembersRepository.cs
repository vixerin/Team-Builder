using TeamBuilder.Common.Functional;
using TeamBuilder.TeamMembers.Application.Enums;
using TeamBuilder.TeamMembers.Application.Models;

namespace TeamBuilder.TeamMembers.Domain;

public interface ITeamMembersRepository
{
    Task<Result<List<TeamMemberViewModel>>> GetTeamMembers(DisplayMode displayMode);
    Task<Result> AddTeamMembers(List<TeamMemberViewModel> teamMemberViewModels);
}

using TeamBuilder.Functional;
using TeamBuilder.TeamMembers.Application.Models;

namespace TeamBuilder.TeamMembers.Domain;

public interface ITeamMembersRepository
{
    Task<Result<List<TeamMemberViewModel>>> GetTeamMembers();
    Task<Result> AddTeamMember(TeamMemberViewModel teamMemberViewModel);
}

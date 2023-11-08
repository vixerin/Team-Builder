using TeamBuilder.Common.Functional;
using TeamBuilder.DTO.Team.Infrastructure;
using TeamBuilder.Validator.Team.Infrastructure;

namespace TeamBuilder.API.Team.Infrastructure
{
    public class InMemoryTeamStorage
    {
        private static List<TeamMemberDto> TeamMembers { get; set; }

        public async Task<Result<List<TeamMemberDto>>> GetAllTeamMembersByTeamId(Guid teamId, bool? isActive)
        {
            if (TeamMembers == null)
            {
                TeamMembers = await GetAllTeamMembers();
            }

            if (TeamMembers.Count == 0)
                return Result.Fail<List<TeamMemberDto>>("Team members list is empty.");

            if (isActive.HasValue == false)
                return Result.Ok(TeamMembers);

            var teamMembers = TeamMembers;

            teamMembers = isActive.Value
                ? teamMembers.Where(x => x.IsActive).ToList()
                : teamMembers.Where(x => x.IsActive == false).ToList();

            return Result.Ok(teamMembers);
        }

        private Task<List<TeamMemberDto>> GetAllTeamMembers()
        {
            var members = new List<TeamMemberDto>
            {
                TeamMemberDto.CreateActive("John Doe", "Joe", "Senior Xamarin Developer"),
                TeamMemberDto.CreateActive("Alice Chain", "Margot", "Lead Designer"),
                TeamMemberDto.CreateActive("Jack Black", "Blacky", "Full Stack .Net Developer"),
                TeamMemberDto.CreateActive("Gerald Earl Gilluze", "G-Eazy", "Project Manager"),
                TeamMemberDto.CreateInactive("Mac Miller", "Mac", "Mobile Consultant"),
                TeamMemberDto.CreateInactive("Amy Jade Whinehouse", "Amy", "Eurockéennes")
            };

            var result = members
                .OrderBy(x => x.Name);

            return Task.FromResult(result.ToList());
        }

        public async Task<Result> AddMembers(Guid teamId, List<TeamMemberDto> teamMembers)
        {
            if(TeamMembers == null)
                TeamMembers = new List<TeamMemberDto>();

            if (teamMembers == null)
                return Result.Fail("New team members list is null.");

            if (teamMembers.Count == 0)
                return Result.Fail("New team members list is empty.");

            foreach (var member in teamMembers)
            {
                var validationResult = TeamMemberValidator.Validate(member.Name, member.NickName, member.Position, member.PhoneNumber);
                if (validationResult.IsFailure)
                    return Result.Fail($"Error adding team member {member.Name}:\r\n{validationResult.Error}");
            }

            foreach (var member in teamMembers)
            {
                TeamMembers.Add(member);
            }

            return Result.Ok();
        }
    }
}


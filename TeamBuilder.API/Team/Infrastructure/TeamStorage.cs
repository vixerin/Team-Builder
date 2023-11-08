using TeamBuilder.Common.Functional;
using TeamBuilder.DTO.Team.Infrastructure;
using TeamBuilder.Validator.Team.Infrastructure;

namespace TeamBuilder.API.Team.Infrastructure
{
    public class InMemoryTeamStorage
    {
        public static List<TeamMemberDto> TeamMembers { get; set; }

        public async Task<Result<List<TeamMemberDto>>> GetAllTeamMembersByTeamId(Guid teamId)
        {
            if (TeamMembers == null)
                TeamMembers = await GetAllTeamMembers();

            if (TeamMembers.Count == 0)
                return Result.Fail<List<TeamMemberDto>>("Team members list is empty.");

            return Result.Ok(TeamMembers);
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

            var result = members.Where(m => m.IsActive)
                .OrderBy(v => v.Name);

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


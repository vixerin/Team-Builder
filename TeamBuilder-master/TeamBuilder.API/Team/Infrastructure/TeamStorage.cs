using TeamBuilder.DTO.Team.Infrastructure;

namespace TeamBuilder.API.Team.Infrastructure
{
    public class InMemoryTeamStorage
    {
        public static List<TeamMemberDto> TeamMembers { get; set; }

        public async Task<List<TeamMemberDto>> GetAllTeamMembersByTeamId(Guid teamId)
        {
            if (TeamMembers == null)
                TeamMembers = await GetAllTeamMembers();

            return TeamMembers;
        }

        private Task<List<TeamMemberDto>> GetAllTeamMembers()
        {
            var inactive1 = TeamMemberDto.Create("Mac Miller", "Mac", "Mobile Consultant");
            inactive1.IsActive = false;

            var inactive2 = TeamMemberDto.Create("Amy Jade Whinehouse", "Amy", "Eurockéennes");
            inactive2.IsActive = false;

            var members = new List<TeamMemberDto>
            {
                TeamMemberDto.Create("John Doe", "Joe", "Senior Xamarin Developer"),
                TeamMemberDto.Create("Alice Chain", "Margot", "Lead Designer"),
                TeamMemberDto.Create("Jack Black", "Blacky", "Full Stack .Net Developer"),
                TeamMemberDto.Create("Gerald Earl Gilluze", "G-Eazy", "Project Manager"),
                inactive1,
                inactive2
            };

            var result = members.Where(m => m.IsActive)
                .OrderBy(v => v.Name);

            return Task.FromResult(result.ToList());
        }

        public async Task AddMembers(List<TeamMemberDto> teamMember)
        {
            if(TeamMembers == null)
                TeamMembers = new List<TeamMemberDto>();

            foreach (var member in teamMember)
            {
                TeamMembers.Add(member);
            }
        }
    }
}


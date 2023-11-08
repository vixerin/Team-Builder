namespace TeamBuilder.DTO.Team.Infrastructure
{
    public record TeamMemberDto(string Name, string NickName, string Position, bool IsActive, string? CountryCode = null,
        string? PhoneNumber = null)
    {
        public static TeamMemberDto Create(string name, string nickName, string position,
            bool isActive, string? countryCode = null, string? phoneNumber = null)
        {
            return new TeamMemberDto(name, nickName, position, isActive, countryCode, phoneNumber);
        }

        public static TeamMemberDto CreateActive(string name, string nickName, string position,
            string? countryCode = null, string? phoneNumber = null)
        {
            return new TeamMemberDto(name, nickName, position, true, countryCode, phoneNumber);
        }

        public static TeamMemberDto CreateInactive(string name, string nickName, string position,
            string? countryCode = null, string? phoneNumber = null)
        {
            return new TeamMemberDto(name, nickName, position, false, countryCode, phoneNumber);
        }
    }
}


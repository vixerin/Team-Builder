namespace TeamBuilder.DTO.Team.Infrastructure
{
    public record TeamMemberDto
    {
        public string Name { get; init; }
        public string NickName { get; init; }
        public string Position { get; init; }
        public string? CountryCode { get; init; }
        public string? PhoneNumber { get; init; }

        public bool IsActive { get; set; }

        public static TeamMemberDto Create(string name, string nickName, string position, string? countryCode = null, string? phoneNumber = null)
        {
            return new TeamMemberDto
            {
                Name = name,
                NickName = nickName,
                Position = position,
                CountryCode = countryCode,
                PhoneNumber = phoneNumber,
                IsActive = true
            };
        }
    }
}


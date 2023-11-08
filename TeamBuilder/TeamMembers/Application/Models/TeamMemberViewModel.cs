using TeamBuilder.DTO.Team.Infrastructure;

namespace TeamBuilder.TeamMembers.Application.Models
{
    public record TeamMemberViewModel
    {
        private TeamMemberViewModel(string name, string nickName, string position, bool isActive, string countryCode, string phoneNumber)
        {
            Name = name;
            NickName = nickName;
            Position = position;
            IsActive = isActive;
            CountryCode = string.IsNullOrWhiteSpace(phoneNumber) ? "" : countryCode;
            PhoneNumber = phoneNumber;
        }

        public string Name { get; }
        public string NickName { get; }
        public string Position { get; }
        public string CountryCode { get; }
        public string PhoneNumber { get; }
        public bool IsActive { get; }

        // ReSharper disable once UnusedMember.Global Used By AddTeamMembersPage and TeamMembersPage
        public string FullPhoneNumber => $"{CountryCode} {PhoneNumber}";

        public static TeamMemberViewModel Create(string name, string nickName, string position, bool isActive, string countryCode = null, string phoneNumber = null)
        {
            return new TeamMemberViewModel(name, nickName, position, isActive, countryCode, phoneNumber);
        }

        public static TeamMemberViewModel CreateActive(string name, string nickName, string position, string countryCode = null, string phoneNumber = null)
        {
            return new TeamMemberViewModel(name, nickName, position, true, countryCode, phoneNumber);
        }
    }

    public static class TeamMemberConverter
    {
        public static TeamMemberViewModel ToViewModel(this TeamMemberDto teamMemberDto)
        {
            return TeamMemberViewModel.Create(teamMemberDto.Name, teamMemberDto.NickName, teamMemberDto.Position,
                teamMemberDto.IsActive, teamMemberDto.CountryCode, teamMemberDto.PhoneNumber);
        }

        public static TeamMemberDto ToDto(this TeamMemberViewModel viewModel)
        {
            return TeamMemberDto.Create(viewModel.Name, viewModel.NickName, viewModel.Position, viewModel.IsActive,
                viewModel.CountryCode, viewModel.PhoneNumber);
        }
    }
}


using TeamBuilder.DTO.Team.Infrastructure;

namespace TeamBuilder.TeamMembers.Application.Models
{
    public class TeamMemberViewModel
    {
        private TeamMemberViewModel(string name, string nickName, string position,
             string countryCode, string phoneNumber)
        {
            Name = name;
            NickName = nickName;
            Position = position;
            CountryCode = countryCode;
            PhoneNumber = phoneNumber;
        }

        public string Name { get; }
        public string NickName { get; }
        public string Position { get; }
        public string CountryCode { get; }
        public string PhoneNumber { get; }
        // ReSharper disable once UnusedMember.Global Used By AddTeamMembersPage and TeamMembersPage
        public string FullPhoneNumber => $"{CountryCode} {PhoneNumber}";

        public static TeamMemberViewModel Create(string name, string nickName, string position, string countryCode = null, string phoneNumber = null)
        {
            return new TeamMemberViewModel(name, nickName, position, countryCode, phoneNumber);
        }
    }

    public static class TeamMemberConverter
    {
        public static TeamMemberViewModel ToViewModel(this TeamMemberDto teamMemberDto)
        {
            return TeamMemberViewModel.Create(teamMemberDto.Name, teamMemberDto.NickName, teamMemberDto.Position, teamMemberDto.CountryCode, teamMemberDto.PhoneNumber);
        }

        public static TeamMemberDto ToDto(this TeamMemberViewModel viewModel)
        {
            return TeamMemberDto.Create(viewModel.Name, viewModel.NickName, viewModel.Position, viewModel.CountryCode, viewModel.PhoneNumber);
        }
    }
}


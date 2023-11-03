﻿namespace TeamBuilder.DTO.Team.Infrastructure
{
    public class TeamMemberDto
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Position { get; set; }

        public bool IsActive { get; set; }

        public static TeamMemberDto Create(string name, string nickName, string position, string? phoneNumber = null)
        {
            return new TeamMemberDto
            {
                Name = name,
                NickName = nickName,
                Position = position,
                PhoneNumber = phoneNumber,
                IsActive = true
            };
        }
    }
}


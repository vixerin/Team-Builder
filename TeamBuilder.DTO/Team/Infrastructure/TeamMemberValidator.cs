using System.Text;
using TeamBuilder.Common.Functional;

namespace TeamBuilder.DTO.Team.Infrastructure
{
    public static class TeamMemberValidator
    {
        private const int NameMaxLength = 64;
        private const int NickNameMaxLength = 64;
        private const int PositionMaxLength = 64;
        private const int PhoneNumberExactLength = 9;

        public static Result Validate(string name, string nickName, string position, string? phoneNumber = null)
        {
            var validationResult = new StringBuilder();
            int errorCounter = 0;

            if (string.IsNullOrWhiteSpace(name))
            {
                errorCounter++;
                validationResult.Append($"{errorCounter}) Name is required.\r\n");
            }

            if (string.IsNullOrWhiteSpace(nickName))
            {
                errorCounter++;
                validationResult.Append($"{errorCounter}) Nickname is required.\r\n");
            }

            if (string.IsNullOrWhiteSpace(position))
            {
                errorCounter++;
                validationResult.Append($"{errorCounter}) Position is required.\r\n");
            }

            if (name?.Length > NameMaxLength)
            {
                errorCounter++;
                validationResult.Append($"{errorCounter}) Maximum length for Name is {NameMaxLength}.\r\n");
            }

            if (nickName?.Length > NickNameMaxLength)
            {
                errorCounter++;
                validationResult.Append($"{errorCounter}) Maximum length for Nickname is {NickNameMaxLength}.\r\n");
            }

            if (position?.Length > PositionMaxLength)
            {
                errorCounter++;
                validationResult.Append($"{errorCounter}) Maximum length for Position is {PositionMaxLength}.\r\n");
            }

            if (!string.IsNullOrWhiteSpace(phoneNumber) && phoneNumber.Length != PhoneNumberExactLength)
            {
                errorCounter++;
                validationResult.Append($"{errorCounter}) Phone number must be exactly {PhoneNumberExactLength} digits.\r\n");
            }

            var finalError = validationResult.ToString();

            if (!string.IsNullOrWhiteSpace(finalError) && errorCounter > 0)
                return Result.Fail(finalError);

            return Result.Ok();
        }
    }
}

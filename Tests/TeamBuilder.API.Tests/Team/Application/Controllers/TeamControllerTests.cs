using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using TeamBuilder.API.Team.Application.Controllers;
using TeamBuilder.DTO.Team.Infrastructure;

namespace TeamBuilder.API.Tests.Team.Application.Controllers
{
    public class TeamControllerTests
    {
        [Fact]
        public async Task AddTeamMember_Return200_WhenMemberIsSuccesfulyAdded()
        {
            //arrange
            var loggerStub = new Mock<ILogger<TeamController>>();
            var controller = new TeamController(loggerStub.Object);

            //act
            var response = await controller.AddTeamMember(Guid.NewGuid(),
                new TeamMemberDto
                {
                    Name = "Tomas",
                    NickName = "Tom",
                    Position = "Senior .NET Developer",
                    IsActive = true
                });

            //assert
            ((IStatusCodeActionResult)response).StatusCode.Should().Be(200);
        }
    }
}


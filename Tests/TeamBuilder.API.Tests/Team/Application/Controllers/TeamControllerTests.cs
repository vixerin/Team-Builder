using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using TeamBuilder.API.Team.Application.Controllers;
using TeamBuilder.DTO.Team.Infrastructure;

namespace TeamBuilder.API.Tests.Team.Application.Controllers
{
    public class TeamControllerTests
    {
        [Fact]
        public async Task AddTeamMember_ReturnsResultSuccess_WhenMemberIsSuccesfulyAdded()
        {
            //arrange
            var loggerStub = new Mock<ILogger<TeamController>>();
            var controller = new TeamController(loggerStub.Object);

            //act
            var response = await controller.AddTeamMembers(Guid.NewGuid(),
                new List<TeamMemberDto> {new("Tomas", "Tom", "Senior .NET Developer", true)});

            //assert
            response.IsSuccess.Should().Be(true);
        }

        [Fact]
        public async Task AddTeamMember_ReturnsResultSuccess_WhenMemberIsSuccesfulyAddedWithPhoneNumber()
        {
            //arrange
            var loggerStub = new Mock<ILogger<TeamController>>();
            var controller = new TeamController(loggerStub.Object);

            //act
            var response = await controller.AddTeamMembers(Guid.NewGuid(),
                new List<TeamMemberDto> {new("Tomas", "Tom", "Senior .NET Developer", true, PhoneNumber: "123222111")});

            //assert
            response.IsSuccess.Should().Be(true);
        }

        [Fact]
        public async Task AddTeamMember_ReturnsResultFailure_WhenMemberPhoneIsIncorrect()
        {
            //arrange
            var loggerStub = new Mock<ILogger<TeamController>>();
            var controller = new TeamController(loggerStub.Object);

            //act
            var response = await controller.AddTeamMembers(Guid.NewGuid(),
                new List<TeamMemberDto> {new("Tomas", "Tom", "Senior .NET Developer", true, PhoneNumber: "48254533")});

            //assert
            response.IsSuccess.Should().Be(false);
        }

        [Fact]
        public async Task AddTeamMember_ReturnsResultFailure_WhenMemberDataIsMissing()
        {
            //arrange
            var loggerStub = new Mock<ILogger<TeamController>>();
            var controller = new TeamController(loggerStub.Object);

            //act
            var response = await controller.AddTeamMembers(Guid.NewGuid(), null);

            //assert
            response.IsSuccess.Should().Be(false);
        }

        [Fact]
        public async Task AddTeamMember_ReturnsResultFailure_WhenMemberDataIsEmpty()
        {
            //arrange
            var loggerStub = new Mock<ILogger<TeamController>>();
            var controller = new TeamController(loggerStub.Object);

            //act
            var response = await controller.AddTeamMembers(Guid.NewGuid(), new List<TeamMemberDto>());

            //assert
            response.IsSuccess.Should().Be(false);
        }

        [Fact]
        public async Task AddTeamMember_ReturnsResultFailure_WhenMemberDataIsIncorrect()
        {
            //arrange
            var loggerStub = new Mock<ILogger<TeamController>>();
            var controller = new TeamController(loggerStub.Object);

            //act
            var response = await controller.AddTeamMembers(Guid.NewGuid(),
                new List<TeamMemberDto> {new("Tomas", "Tom", null, true)});

            //assert
            response.IsSuccess.Should().Be(false);
        }
    }
}


using TeamBuilder.TeamMembers.Application.Interfaces;
using TeamBuilder.TeamMembers.Application.Services;
using TeamBuilder.TeamMembers.Application.ViewModels;
using TeamBuilder.TeamMembers.Application.Views;
using TeamBuilder.TeamMembers.Domain;
using TeamBuilder.TeamMembers.Infrastructure;

namespace TeamBuilder
{
    public static class PrismStartup
    {
        public static void Configure(PrismAppBuilder builder)
        {
            builder.RegisterTypes(RegisterTypes)
                .OnAppStart("TeamMembersPage");
        }

        private static void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TeamMembersPage, TeamMembersViewModel>();
            containerRegistry.RegisterForNavigation<AddTeamMembersPage, AddTeamMembersViewModel>();

            containerRegistry.Register<ITeamMembersRepository, TeamMembersRepository>();
            containerRegistry.Register<IAlertService, AlertService>();
        }
    }
}

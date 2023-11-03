using TeamBuilder.TeamMembers.Application.AddTeamMembers;
using TeamBuilder.TeamMembers.Application.Interfaces;
using TeamBuilder.TeamMembers.Application.Servoces;
using TeamBuilder.TeamMembers.Application.TeamMembers;
using TeamBuilder.TeamMembers.Domain;
using TeamBuilder.TeamMembers.Infrastructure;
using TeamBuilder.ViewModels;
using TeamBuilder.Views;

namespace TeamBuilder
{
    public static class PrismStartup
    {
        public static void Configure(PrismAppBuilder builder)
        {
            builder.RegisterTypes(RegisterTypes)
                .OnAppStart("MainPage");
        }

        private static void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<TeamMembersPage, TeamMembersViewModel>();
            containerRegistry.RegisterForNavigation<AddTeamMembersPage, AddTeamMembersViewModel>();

            containerRegistry.Register<ITeamMembersRepository, TeamMembersRepository>();
            containerRegistry.Register<IAlertService, AlertService>();
        }
    }
}

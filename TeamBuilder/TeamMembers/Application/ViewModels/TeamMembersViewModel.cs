using System.Collections.ObjectModel;
using TeamBuilder.TeamMembers.Application.Interfaces;
using TeamBuilder.TeamMembers.Application.Models;
using TeamBuilder.TeamMembers.Application.ViewModels.Base;
using TeamBuilder.TeamMembers.Domain;

namespace TeamBuilder.TeamMembers.Application.ViewModels
{
    public class TeamMembersViewModel : BaseViewModel
    {
        private readonly ITeamMembersRepository _teamMembersRepository;

        public TeamMembersViewModel(INavigationService navigationService, IAlertService alertService,
            ITeamMembersRepository teamMembersRepository) : base(navigationService, alertService)
        {
            _teamMembersRepository = teamMembersRepository;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                await LoadTeamMembers();
            }
            catch (Exception ex)
            {
                await AlertService.ShowAlertDialogAsync("Error", ex.Message);
            }
        }

        private async Task LoadTeamMembers()
        {
            TeamMemberViewModels = new ObservableCollection<TeamMemberViewModel>();

            var membersResult = await _teamMembersRepository.GetTeamMembers();
            if (membersResult.IsFailure)
            {
                await AlertService.ShowAlertDialogAsync("Error", membersResult.Error);
                return;
            }

            foreach (var member in membersResult.Value)
            {
                TeamMemberViewModels.Add(member);
            }
        }

        private DelegateCommand _addNewMembersCommand;
        public DelegateCommand AddNewMembersCommand => _addNewMembersCommand ??= new DelegateCommand(async () => await ExecuteAddNewMembersCommand());

        private async Task ExecuteAddNewMembersCommand()
        {
            await NavigationService.NavigateAsync("AddTeamMembersPage");
        }

        private ObservableCollection<TeamMemberViewModel> _teamMemberViewModels;
        public ObservableCollection<TeamMemberViewModel> TeamMemberViewModels
        {
            get => _teamMemberViewModels;
            set => SetProperty(ref _teamMemberViewModels, value);
        }
    }
}


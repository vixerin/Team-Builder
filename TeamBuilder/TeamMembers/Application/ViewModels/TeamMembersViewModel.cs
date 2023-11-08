using System.Collections.ObjectModel;
using TeamBuilder.TeamMembers.Application.Enums;
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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            DisplayModes = new List<DisplayMode>
            {
                DisplayMode.All,
                DisplayMode.Active,
                DisplayMode.Inactive,
            };
            SelectedDisplayMode = DisplayModes.First();
        }

        private async Task LoadTeamMembers()
        {
            try
            {
                TeamMemberViewModels = new ObservableCollection<TeamMemberViewModel>();

                var membersResult = await _teamMembersRepository.GetTeamMembers(SelectedDisplayMode ?? DisplayMode.All);
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
            catch (Exception ex)
            {
                await AlertService.ShowAlertDialogAsync("Error", ex.Message);
            }
        }

        private DelegateCommand _addNewMembersCommand;
        public DelegateCommand AddNewMembersCommand => _addNewMembersCommand ??= new DelegateCommand(async () => await ExecuteAddNewMembersCommand());

        private async Task ExecuteAddNewMembersCommand()
        {
            await NavigationService.NavigateAsync("AddTeamMembersPage");
        }

        private DelegateCommand _displayModeIndexChangedCommand;
        public DelegateCommand DisplayModeIndexChangedCommand => _displayModeIndexChangedCommand ??= new DelegateCommand(async () => await ExecuteDisplayModeIndexChangedCommand());

        private async Task ExecuteDisplayModeIndexChangedCommand()
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

        private ObservableCollection<TeamMemberViewModel> _teamMemberViewModels;
        public ObservableCollection<TeamMemberViewModel> TeamMemberViewModels
        {
            get => _teamMemberViewModels;
            set => SetProperty(ref _teamMemberViewModels, value);
        }

        private List<DisplayMode> _displayModes;
        public List<DisplayMode> DisplayModes
        {
            get => _displayModes;
            set => SetProperty(ref _displayModes, value);
        }

        private DisplayMode? _selectedDisplayMode;
        public DisplayMode? SelectedDisplayMode
        {
            get => _selectedDisplayMode;
            set => SetProperty(ref _selectedDisplayMode, value);
        }
    }
}


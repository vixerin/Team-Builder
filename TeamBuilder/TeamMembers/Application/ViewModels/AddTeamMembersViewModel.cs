using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using Mopups.Interfaces;
using TeamBuilder.TeamMembers.Application.Dialogs;
using TeamBuilder.TeamMembers.Application.Interfaces;
using TeamBuilder.TeamMembers.Application.Models;
using TeamBuilder.TeamMembers.Application.ViewModels.Base;
using TeamBuilder.TeamMembers.Domain;

namespace TeamBuilder.TeamMembers.Application.ViewModels
{
    public class AddTeamMembersViewModel : BaseViewModel
    {
        private readonly ITeamMembersRepository _teamMembersRepository;
        private readonly IPopupNavigation _popupNavigation;

        public AddTeamMembersViewModel(INavigationService navigationService, IAlertService alertService,
            ITeamMembersRepository teamMembersRepository, IPopupNavigation popupNavigation) : base(navigationService, alertService)
        {
            _teamMembersRepository = teamMembersRepository;
            _popupNavigation = popupNavigation;
            NewMemberViewModels = new ObservableCollection<TeamMemberViewModel>();
            IsTeamMemberSelected = false;
        }

        private DelegateCommand _addNewTeamMemberCommand;
        public DelegateCommand AddNewTeamMemberCommand => _addNewTeamMemberCommand ??= new DelegateCommand(async () => await ExecuteAddNewTeamMemberCommand());

        private async Task ExecuteAddNewTeamMemberCommand()
        {
            try
            {
                var dialogParameters = new DialogParameters { { "TeamMembers", NewMemberViewModels.ToList() } };
                var addTeamMemberDialog = new AddTeamMemberDialog(dialogParameters, NavigationService, AlertService);
                await _popupNavigation.PushAsync(addTeamMemberDialog);

                var result = await addTeamMemberDialog.PopupDismissedTask;

                if (result.ContainsKey("AddSuccessful") && result.ContainsKey("NewTeamMember") && result.GetValue<bool>("AddSuccessful"))
                {
                    var newTeamMemberViewModel = result.GetValue<TeamMemberViewModel>("NewTeamMember");
                    NewMemberViewModels.Add(newTeamMemberViewModel);
                }
            }
            catch (Exception ex)
            {
                await AlertService.ShowAlertDialogAsync("Error", ex.Message);
            }
        }

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand ??= new DelegateCommand(async () => await ExecuteSaveCommand());

        private async Task ExecuteSaveCommand()
        {
            try
            {
                if (NewMemberViewModels.Any() == false)
                {
                    await AlertService.ShowAlertDialogAsync("Warning", "No team members were added.");
                    return;
                }

                var currentTeamMembersResult = await _teamMembersRepository.GetTeamMembers();
                if (currentTeamMembersResult.IsFailure)
                {
                    await AlertService.ShowAlertDialogAsync("Error", currentTeamMembersResult.Error);
                    return;
                }

                foreach (var currentTeamMember in currentTeamMembersResult.Value)
                {
                    if (NewMemberViewModels.Any(x => x.Name == currentTeamMember.Name))
                    {
                        await AlertService.ShowAlertDialogAsync("Error",
                            $"Team member with the same name ({currentTeamMember.Name}) has already been added to the list.");
                        return;
                    }
                }

                var addTeamMembersResult = await _teamMembersRepository.AddTeamMembers(NewMemberViewModels.ToList());
                if (addTeamMembersResult.IsFailure)
                {
                    await AlertService.ShowAlertDialogAsync("Error", addTeamMembersResult.Error);
                    return;
                }

                await NavigationService.NavigateAsync("TeamMembersPage");
            }
            catch (Exception ex)
            {
                await AlertService.ShowAlertDialogAsync("Error", ex.Message);
            }
        }

        private DelegateCommand _removeCommand;
        public DelegateCommand RemoveCommand => _removeCommand ??= new DelegateCommand(async () => await ExecuteRemoveCommand());

        private async Task ExecuteRemoveCommand()
        {
            try
            {
                if (SelectedTeamMemberViewModel == null)
                    return;

                var questionResult = await AlertService.ShowQuestionDialogAsync("Question", "Are you sure you want to remove selected team member from the list?");
                if (questionResult == false)
                    return;

                NewMemberViewModels.Remove(SelectedTeamMemberViewModel);
                SelectedTeamMemberViewModel = null;
            }
            catch (Exception ex)
            {
                await AlertService.ShowAlertDialogAsync("Error", ex.Message);
            }
        }

        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand => _cancelCommand ??= new DelegateCommand(async () => await ExecuteCancelCommand());

        private async Task ExecuteCancelCommand()
        {
            try
            {
                var questionResult = await AlertService.ShowQuestionDialogAsync("Question", "Are you sure you want to cancel?");
                if (questionResult == false)
                    return;

                await NavigationService.NavigateAsync("TeamMembersPage");
            }
            catch (Exception ex)
            {
                await AlertService.ShowAlertDialogAsync("Error", ex.Message);
            }
        }

        private ObservableCollection<TeamMemberViewModel> _newMemberViewModels;
        public ObservableCollection<TeamMemberViewModel> NewMemberViewModels
        {
            get => _newMemberViewModels;
            set => SetProperty(ref _newMemberViewModels, value);
        }

        private TeamMemberViewModel _selectedTeamMemberViewModel;
        public TeamMemberViewModel SelectedTeamMemberViewModel
        {
            get => _selectedTeamMemberViewModel;
            set => SetProperty(ref _selectedTeamMemberViewModel, value, () =>
            {
                IsTeamMemberSelected = SelectedTeamMemberViewModel != null;
            });
        }

        private bool _isTeamMemberSelected;
        public bool IsTeamMemberSelected
        {
            get => _isTeamMemberSelected;
            set => SetProperty(ref _isTeamMemberSelected, value);
        }
    }
}

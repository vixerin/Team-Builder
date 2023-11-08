using Mopups.Services;
using Prism.Services.Dialogs;
using TeamBuilder.TeamMembers.Application.Interfaces;
using TeamBuilder.TeamMembers.Application.Models;
using TeamBuilder.TeamMembers.Application.ViewModels.Base;
using TeamBuilder.Validator.Team.Infrastructure;

namespace TeamBuilder.TeamMembers.Application.Dialogs
{
    public class AddTeamMemberDialogViewModel : BaseViewModel
    {
        private readonly IDialogAware _dialogAware;
        private readonly List<TeamMemberViewModel> _newMemberViewModels;

        public AddTeamMemberDialogViewModel(IDialogAware dialogAware, INavigationService navigationService, IAlertService alertService, IDialogParameters parameters) 
            : base(navigationService, alertService)
        {
            _dialogAware = dialogAware;

            if (parameters.ContainsKey("TeamMembers"))
            {
                _newMemberViewModels = parameters.GetValue<List<TeamMemberViewModel>>("TeamMembers");
            }

            CountryCodes = new List<string> {"+48"};
            SelectedCountryCode = CountryCodes.FirstOrDefault();
        }

        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand => _confirmCommand ??= new DelegateCommand(async () => await ExecuteConfirmCommand());

        private async Task ExecuteConfirmCommand()
        {
            try
            {
                var validationResult = TeamMemberValidator.Validate(Name, Nickname, Position, PhoneNumber);
                if (validationResult.IsFailure)
                {
                    await AlertService.ShowAlertDialogAsync("Error", validationResult.Error);
                    return;
                }

                if (_newMemberViewModels.Any(x => x.Name == Name))
                {
                    await AlertService.ShowAlertDialogAsync("Error", $"Team member with the same name ({Name}) has already been added to the list.");
                    return;
                }

                var teamMemberViewModel = TeamMemberViewModel.CreateActive(Name, Nickname, Position, SelectedCountryCode, PhoneNumber);

                await MopupService.Instance.PopAsync();
                _dialogAware.RequestClose(new DialogParameters
                {
                    {"NewTeamMember", teamMemberViewModel},
                    {"AddSuccessful", true}
                });
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

                await MopupService.Instance.PopAsync();
                _dialogAware.RequestClose(new DialogParameters
                {
                    {"NewTeamMember", null},
                    {"AddSuccessful", false}
                });
            }
            catch (Exception ex)
            {
                await AlertService.ShowAlertDialogAsync("Error", ex.Message);
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _nickname;
        public string Nickname
        {
            get => _nickname;
            set => SetProperty(ref _nickname, value);
        }

        private string _position;
        public string Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        private List<string> _countryCodes;
        public List<string> CountryCodes
        {
            get => _countryCodes;
            set => SetProperty(ref _countryCodes, value);
        }

        private string _selectedCountryCode;
        public string SelectedCountryCode
        {
            get => _selectedCountryCode;
            set => SetProperty(ref _selectedCountryCode, value);
        }
    }
}

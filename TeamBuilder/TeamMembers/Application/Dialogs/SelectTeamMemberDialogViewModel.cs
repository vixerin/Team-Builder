using Prism.Services.Dialogs;
using TeamBuilder.TeamMembers.Application.Interfaces;
using TeamBuilder.TeamMembers.Application.Models;

namespace TeamBuilder.TeamMembers.Application.Dialogs
{
    public class SelectTeamMemberDialogViewModel : BindableBase
    {
        private readonly List<TeamMemberViewModel> _originalTeamMemberViewModels;
        private readonly IDialogAware _dialogAware;

        public SelectTeamMemberDialogViewModel(IDialogAware dialogAware, IDialogParameters parameters)
        {
            _dialogAware = dialogAware;

            if (parameters.ContainsKey("TeamMembersList"))
            {
                _originalTeamMemberViewModels = parameters.GetValue<IEnumerable<TeamMemberViewModel>>("TeamMembersList").ToList();
                TeamMemberViewModels = _originalTeamMemberViewModels;
            }
        }

        public void SearchBar_TextChanged(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                TeamMemberViewModels = _originalTeamMemberViewModels;
            }
            else
            {
                TeamMemberViewModels = _originalTeamMemberViewModels.Where(x => x.Name.ToLower().Contains(query.ToLower())).ToList();
            }
        }

        private DelegateCommand _selectTeamMemberCommand;
        public DelegateCommand SelectTeamMemberCommand => _selectTeamMemberCommand ??= new DelegateCommand(ExecuteSelectTeamMemberCommand);

        private void ExecuteSelectTeamMemberCommand()
        {
            if (SelectedTeamMemberViewModel != null)
            {
                _dialogAware.RequestClose(new DialogParameters { { "Selected", true }, { "SelectedTeamMemberViewModel", SelectedTeamMemberViewModel } });
            }
        }

        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand => _cancelCommand ??= new DelegateCommand(ExecuteCancelCommand);

        private void ExecuteCancelCommand()
        {
            _dialogAware.RequestClose(new DialogParameters { { "Selected", false }, { "SelectedTeamMemberViewModel", null } });
        }

        private List<TeamMemberViewModel> _teamMemberViewModels;
        public List<TeamMemberViewModel> TeamMemberViewModels
        {
            get => _teamMemberViewModels;
            set => SetProperty(ref _teamMemberViewModels, value);
        }

        private TeamMemberViewModel _selectedTeamMemberViewModel;
        public TeamMemberViewModel SelectedTeamMemberViewModel
        {
            get => _selectedTeamMemberViewModel;
            set => SetProperty(ref _selectedTeamMemberViewModel, value);
        }
    }
}

using Mopups.Pages;
using Prism.Services.Dialogs;
using TeamBuilder.TeamMembers.Application.Interfaces;

namespace TeamBuilder.TeamMembers.Application.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTeamMemberDialog : PopupPage, IDialogAware
    {
        private readonly TaskCompletionSource<IDialogParameters> _taskCompletionSource;
        public Task<IDialogParameters> PopupDismissedTask => _taskCompletionSource.Task;

        public AddTeamMemberDialog(IDialogParameters parameters, INavigationService navigationService, IAlertService alertService)
        {
            InitializeComponent();
            BindingContext = new AddTeamMemberDialogViewModel(this, navigationService, alertService, parameters);
            _taskCompletionSource = new TaskCompletionSource<IDialogParameters>();
        }

        public void RequestClose(IDialogParameters parameters)
        {
            _taskCompletionSource.SetResult(parameters);
        }
    }
}

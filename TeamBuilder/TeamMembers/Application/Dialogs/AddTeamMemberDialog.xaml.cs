using CommunityToolkit.Maui.Views;
using Prism.Services.Dialogs;
using TeamBuilder.TeamMembers.Application.Interfaces;

namespace TeamBuilder.TeamMembers.Application.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTeamMemberDialog : Popup, IDialogAware
    {
        public AddTeamMemberDialog(IDialogParameters parameters, INavigationService navigationService, IAlertService alertService)
        {
            InitializeComponent();
            BindingContext = new AddTeamMemberDialogViewModel(this, navigationService, alertService, parameters);
        }

        public void RequestClose(IDialogParameters parameters)
        {
            Close(parameters);
        }
    }
}

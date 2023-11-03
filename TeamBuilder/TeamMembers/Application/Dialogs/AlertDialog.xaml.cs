using CommunityToolkit.Maui.Views;
using Prism.Services.Dialogs;
using TeamBuilder.TeamMembers.Application.Interfaces;

namespace TeamBuilder.TeamMembers.Application.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertDialog : Popup, IDialogAware
    {
        public AlertDialog(IDialogParameters parameters)
        {
            InitializeComponent();
            BindingContext = new AlertDialogViewModel(this, parameters);
        }

        public void RequestClose(IDialogParameters parameters)
        {
            Close(parameters);
        }
    }
}

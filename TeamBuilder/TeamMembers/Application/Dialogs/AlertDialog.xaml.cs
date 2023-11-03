using Mopups.Pages;
using Prism.Services.Dialogs;

namespace TeamBuilder.TeamMembers.Application.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertDialog : PopupPage
    {
        public AlertDialog(IDialogParameters parameters)
        {
            InitializeComponent();
            BindingContext = new AlertDialogViewModel(parameters);
        }
    }
}

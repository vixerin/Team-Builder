using CommunityToolkit.Maui.Views;
using Prism.Services.Dialogs;
using TeamBuilder.TeamMembers.Application.Interfaces;

namespace TeamBuilder.TeamMembers.Application.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionDialog : Popup, IDialogAware
    {
        public QuestionDialog(IDialogParameters parameters)
        {
            InitializeComponent();
            BindingContext = new QuestionDialogViewModel(this, parameters);
        }

        public void RequestClose(IDialogParameters parameters)
        {
            Close(parameters);
        }
    }
}

using CommunityToolkit.Maui.Views;
using Prism.Services.Dialogs;

namespace TeamBuilder.Dialogs
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

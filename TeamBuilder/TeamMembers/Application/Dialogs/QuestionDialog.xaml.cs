using Mopups.Pages;
using Prism.Services.Dialogs;
using TeamBuilder.TeamMembers.Application.Interfaces;

namespace TeamBuilder.TeamMembers.Application.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionDialog : PopupPage, IDialogAware
    {
        private readonly TaskCompletionSource<IDialogParameters> _taskCompletionSource;
        public Task<IDialogParameters> PopupDismissedTask => _taskCompletionSource.Task;

        public QuestionDialog(IDialogParameters parameters)
        {
            InitializeComponent();
            BindingContext = new QuestionDialogViewModel(this, parameters);
            _taskCompletionSource = new TaskCompletionSource<IDialogParameters>();
        }

        public void RequestClose(IDialogParameters parameters)
        {
            _taskCompletionSource.SetResult(parameters);
        }
    }
}

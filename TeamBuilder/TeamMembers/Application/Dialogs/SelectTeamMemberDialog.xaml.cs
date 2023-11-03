using CommunityToolkit.Maui.Views;
using Prism.Services.Dialogs;
using TeamBuilder.TeamMembers.Application.Interfaces;

namespace TeamBuilder.TeamMembers.Application.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectTeamMemberDialog : Popup, IDialogAware 
    {
        private readonly SelectTeamMemberDialogViewModel _viewModel;

        public SelectTeamMemberDialog(IDialogParameters parameters)
        {
            InitializeComponent();

            BindingContext = new SelectTeamMemberDialogViewModel(this, parameters);
            
            _viewModel = (SelectTeamMemberDialogViewModel)BindingContext;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SearchBar_TextChanged(e.NewTextValue);
        }

        public void RequestClose(IDialogParameters parameters)
        {
            Close(parameters);
        }
    }
}

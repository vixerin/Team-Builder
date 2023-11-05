using Mopups.Services;
using Prism.Services.Dialogs;

namespace TeamBuilder.TeamMembers.Application.Dialogs
{
    public class AlertDialogViewModel : BindableBase
    {
        public AlertDialogViewModel(IDialogParameters dialogParameters)
        {
            if (dialogParameters.ContainsKey("Title"))
            {
                TitleLabel = dialogParameters.GetValue<string>("Title");
            }

            if (dialogParameters.ContainsKey("Info"))
            {
                InfoLabel = dialogParameters.GetValue<string>("Info");
            }
        }

        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand => _confirmCommand ??= new DelegateCommand(ExecuteConfirmCommand);

        private void ExecuteConfirmCommand()
        {
            MopupService.Instance.PopAsync();
        }

        private string _tittle = "";
        public string TitleLabel
        {
            get => _tittle;
            set => SetProperty(ref _tittle, value);
        }

        private string _infoLabel = "";
        public string InfoLabel
        {
            get => _infoLabel;
            set => SetProperty(ref _infoLabel, value);
        }
    }
}

using Prism.Services.Dialogs;
using TeamBuilder.TeamMembers.Application.Interfaces;

namespace TeamBuilder.TeamMembers.Application.Dialogs
{
    public class AlertDialogViewModel : BindableBase
    {
        private readonly IDialogAware _dialogAware;

        public AlertDialogViewModel(IDialogAware dialogAware, IDialogParameters parameters)
        {
            _dialogAware = dialogAware;

            if (parameters.ContainsKey("Title"))
            {
                TitleLabel = parameters.GetValue<string>("Title");
            }

            if (parameters.ContainsKey("Info"))
            {
                InfoLabel = parameters.GetValue<string>("Info");
            }
        }

        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand => _confirmCommand ??= new DelegateCommand(ExecuteConfirmCommand);

        private void ExecuteConfirmCommand()
        {
            _dialogAware.RequestClose(new DialogParameters());
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

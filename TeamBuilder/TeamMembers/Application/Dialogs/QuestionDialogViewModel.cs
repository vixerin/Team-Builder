using Mopups.Services;
using Prism.Services.Dialogs;
using TeamBuilder.TeamMembers.Application.Interfaces;

namespace TeamBuilder.TeamMembers.Application.Dialogs
{
    public class QuestionDialogViewModel : BindableBase
    {
        private readonly IDialogAware _dialogAware;

        public QuestionDialogViewModel(IDialogAware dialogAware, IDialogParameters dialogParameters)
        {
            _dialogAware = dialogAware;

            if (dialogParameters.ContainsKey("Title"))
            {
                TitleLabel = dialogParameters.GetValue<string>("Title");
            }

            if (dialogParameters.ContainsKey("Info"))
            {
                InfoLabel = dialogParameters.GetValue<string>("Info");
            }
        }

        private DelegateCommand _yesCommand;
        public DelegateCommand YesCommand => _yesCommand ??= new DelegateCommand(async () => await ExecuteYesCommand());

        private async Task ExecuteYesCommand()
        {
            await MopupService.Instance.PopAsync();
            _dialogAware.RequestClose(new DialogParameters {{"ResultAnswer", true}});
        }

        private DelegateCommand _noCommand;
        public DelegateCommand NoCommand => _noCommand ??= new DelegateCommand(async () => await ExecuteNoCommand());

        private async Task ExecuteNoCommand()
        {
            await MopupService.Instance.PopAsync();
            _dialogAware.RequestClose(new DialogParameters {{"ResultAnswer", false}});
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

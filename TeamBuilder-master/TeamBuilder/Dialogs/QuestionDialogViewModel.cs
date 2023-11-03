using Prism.Services.Dialogs;

namespace TeamBuilder.Dialogs
{
    public class QuestionDialogViewModel : BindableBase
    {
        private readonly IDialogAware _dialogAware;

        public QuestionDialogViewModel(IDialogAware dialogAware, IDialogParameters parameters)
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

        private DelegateCommand _yesCommand;
        public DelegateCommand YesCommand => _yesCommand ??= new DelegateCommand(ExecuteYesCommand);

        private void ExecuteYesCommand()
        {
            _dialogAware.RequestClose(new DialogParameters {{"ResultAnswer", true}});
        }

        private DelegateCommand _noCommand;
        public DelegateCommand NoCommand => _noCommand ??= new DelegateCommand(ExecuteNoCommand);

        private void ExecuteNoCommand()
        {
            _dialogAware.RequestClose(new DialogParameters { { "ResultAnswer", false } });
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

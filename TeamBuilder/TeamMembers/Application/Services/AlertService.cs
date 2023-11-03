using Mopups.Interfaces;
using Prism.Services.Dialogs;
using TeamBuilder.TeamMembers.Application.Dialogs;
using TeamBuilder.TeamMembers.Application.Interfaces;

namespace TeamBuilder.TeamMembers.Application.Services
{
    public class AlertService : IAlertService
    {
        private readonly IPopupNavigation _popupNavigation;

        public AlertService(IPopupNavigation popupNavigation)
        {
            _popupNavigation = popupNavigation;
        }

        public async Task ShowAlertDialogAsync(string title, string info)
        {
            var dialogParameters = new DialogParameters
            {
                {"Title", title},
                {"Info", info}
            };

            await _popupNavigation.PushAsync(new AlertDialog(dialogParameters));
        }

        public async Task<bool> ShowQuestionDialogAsync(string title, string info)
        {
            var dialogParameters = new DialogParameters
            {
                {"Title", title},
                {"Info", info}
            };

            var questionDialog = new QuestionDialog(dialogParameters);
            await _popupNavigation.PushAsync(questionDialog);

            var result = await questionDialog.PopupDismissedTask;

            return result.ContainsKey("ResultAnswer") && result.GetValue<bool>("ResultAnswer");
        }
    }
}

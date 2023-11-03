using CommunityToolkit.Maui.Views;
using Prism.Services.Dialogs;
using TeamBuilder.TeamMembers.Application.Dialogs;
using TeamBuilder.TeamMembers.Application.Interfaces;

namespace TeamBuilder.TeamMembers.Application.Services
{
    public class AlertService : IAlertService
    {
        public async Task ShowAlertDialogAsync(string title, string info)
        {
            var dialogParameters = new DialogParameters
            {
                {"Title", title},
                {"Info", info}
            };

            await App.Current.MainPage.ShowPopupAsync(new AlertDialog(dialogParameters));
        }

        public async Task<bool> ShowQuestionDialogAsync(string title, string info)
        {
            var dialogParameters = new DialogParameters
            {
                {"Title", title},
                {"Info", info}
            };

            var result = (IDialogParameters) await App.Current.MainPage.ShowPopupAsync(new QuestionDialog(dialogParameters));
            return result.ContainsKey("ResultAnswer") && result.GetValue<bool>("ResultAnswer");
        }
    }
}

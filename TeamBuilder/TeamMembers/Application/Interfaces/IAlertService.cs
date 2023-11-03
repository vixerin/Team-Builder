namespace TeamBuilder.TeamMembers.Application.Interfaces
{
    public interface IAlertService
    {
        Task ShowAlertDialogAsync(string title, string info);
        Task<bool> ShowQuestionDialogAsync(string title, string info);
    }
}

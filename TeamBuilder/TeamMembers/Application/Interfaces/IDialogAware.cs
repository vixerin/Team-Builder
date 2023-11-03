using Prism.Services.Dialogs;

namespace TeamBuilder.TeamMembers.Application.Interfaces
{
    public interface IDialogAware
    {
        void RequestClose(IDialogParameters parameters);
    }
}

using Prism.Services.Dialogs;

namespace TeamBuilder.Dialogs
{
    public interface IDialogAware
    {
        void RequestClose(IDialogParameters parameters);
    }
}

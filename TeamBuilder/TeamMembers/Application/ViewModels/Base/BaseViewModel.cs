using Microsoft.Extensions.Logging;
using TeamBuilder.TeamMembers.Application.Interfaces;

namespace TeamBuilder.TeamMembers.Application.ViewModels.Base
{
    public abstract class BaseViewModel : BindableBase, INavigationAware
    {
        protected readonly INavigationService NavigationService;
        protected readonly IAlertService AlertService;

        protected BaseViewModel(INavigationService navigationService, IAlertService alertService)
        {
            NavigationService = navigationService;
            AlertService = alertService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }
    }
}
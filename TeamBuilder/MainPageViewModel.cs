namespace TeamBuilder;

public class MainPageViewModel : BindableBase
{
    public MainPageViewModel(INavigationService navigationService)
    {
        navigationService.NavigateAsync("TeamMembersPage");
    }
}

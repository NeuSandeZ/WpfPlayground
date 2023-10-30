using Hotel.MVVM.ViewModels;
using Hotel.Services.Interfaces;
using Hotel.Stores;

namespace Hotel.Services;

public class NavigationService : INavigationService
{
    private readonly NavigationStore _navigationStore;

    public NavigationService(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }

    public ViewModelBase Navigate(View view)
    {
        return _navigationStore.CurrentViewModel = GetCurrentView(view);
    }

    private ViewModelBase GetCurrentView(View view)
    {
        switch (view)
        {
            case View.ListingViewModel:
                return new AccountsListingViewModel();
            case View.TestViewModel:
                return new TestViewModel();
            default:
                return null;
        }
    }
}
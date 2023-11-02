using Hotel.MVVM.ViewModels;
using Hotel.Services.Interfaces;
using Hotel.Stores;

namespace Hotel.Services;

public class NavigationService : INavigationService
{
    private readonly NavigationViewStore _navigationViewStore;

    public NavigationService(NavigationViewStore navigationViewStore)
    {
        _navigationViewStore = navigationViewStore;
    }

    public ViewModelBase Navigate(View view)
    {
        return _navigationViewStore.CurrentViewModel = GetCurrentView(view);
    }

    private ViewModelBase GetCurrentView(View view)
    {
        switch (view)
        {
            case View.ListingViewModel:
                return new ReservationsListingViewModel();
            case View.TestViewModel:
                return new TestViewModel();
            default:
                return null;
        }
    }
}
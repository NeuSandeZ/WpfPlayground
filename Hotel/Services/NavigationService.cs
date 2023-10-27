using Hotel.MVVM.ViewModels;
using Hotel.Services.Interfaces;

namespace Hotel.Services;

public class NavigationService : INavigationService
{
    public ViewModelBase Navigate(View view)
    {
        switch (view)
        {
            case View.ListingViewModel:
                return new AccountsListingViewModel();
                break;
            case View.TestViewModel:
                return new TestViewModel();
                break;
            default:
                return null;
        }
    }
}
using Hotel.MVVM.ViewModels;

namespace Hotel.Services.Interfaces;

public interface INavigationService
{
    ViewModelBase Navigate(View view);
}
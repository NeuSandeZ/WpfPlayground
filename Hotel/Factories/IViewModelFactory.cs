using Hotel.MVVM.ViewModels;
using Hotel.Services;

namespace Hotel.Factories;

public interface IViewModelFactory
{
    ViewModelBase CreateViewModel(ViewType viewType);
}
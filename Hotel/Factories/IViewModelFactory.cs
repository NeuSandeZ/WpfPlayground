using Hotel.MVVM.ViewModels;
using Hotel.Services.Interfaces;

namespace Hotel.Factories;

public interface IViewModelFactory
{
    ViewModelBase CreateViewModel(ViewType viewType);
}
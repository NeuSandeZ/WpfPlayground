using System.ComponentModel;
using System.Runtime.CompilerServices;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels;

public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;

public class ViewModelBase : INotifyPropertyChanged
{
    // public virtual void Dispose() { }
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
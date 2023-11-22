using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Hotel.MVVM.ViewModels;


public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public virtual void Dispose()
    {
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
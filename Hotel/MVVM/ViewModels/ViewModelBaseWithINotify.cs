using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Hotel.MVVM.ViewModels;

public class ViewModelBaseWithINotify : ViewModelBase, INotifyDataErrorInfo
{
    protected readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;

    protected ViewModelBaseWithINotify()
    {
        _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
    }

    public IEnumerable GetErrors(string? propertyName)
    {
        return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
    }

    public bool HasErrors => _propertyNameToErrorsDictionary.Any();
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    protected void ClearErrors(string propertyName)
    {
        _propertyNameToErrorsDictionary.Remove(propertyName);
        OnErrorsChanged(propertyName);
    }

    protected void AddError(string errorMessage, string propertyName)
    {
        if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
        _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);
        OnErrorsChanged(propertyName);
    }

    protected void OnErrorsChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}
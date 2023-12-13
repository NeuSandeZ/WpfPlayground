using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Hotel.MVVM.ViewModels;

public abstract class SortingAndFilteringViewModel : ViewModelBase
{
    public ObservableCollection<string> SortComboBoxList { get; set; }
    protected abstract List<string> FilterComboBoxList();
    public abstract string FilterField { get; set; }
    public abstract string ChoosenFilterField { get; set; }
    public abstract ICollectionView CollectionView { get; set; }
    protected abstract bool Filter(object obj);
}
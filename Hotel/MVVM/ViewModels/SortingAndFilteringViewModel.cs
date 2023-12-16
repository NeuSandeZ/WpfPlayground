using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Hotel.Commands;

namespace Hotel.MVVM.ViewModels;

public abstract class SortingAndFilteringViewModel : ViewModelBase
{
    // Filtering
    public ObservableCollection<string> FilterComboBoxList { get; set; }
    protected abstract List<string> LoadFilterComboBoxList();
    public abstract string FilterField { get; set; }
    public abstract string ChoosenFilterField { get; set; }
    public abstract ICollectionView CollectionView { get; set; }
    protected abstract bool Filter(object obj);

    // Sorting

    public ObservableCollection<string> SortComboBoxList { get; set; }
    protected abstract List<string> LoadSortComboBoxList();
    
    protected string _choosenSortField;
    public abstract string? ChoosenSortField { get; set; }
    protected ComboBoxItem _choosenDirectionSort { get; set; }
    public abstract ComboBoxItem ChoosenDirectionSort { get; set; }
    protected abstract void Sort();
    private BaseCommand _sortCommand;
    public ICommand SortCommand 
    {
        get
        {
            if (_sortCommand is null)
            {
                _sortCommand = new ActionBaseCommand(Sort);
            }

            return _sortCommand;
        }
    }
}
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Hotel.Commands;

namespace Hotel.MVVM.ViewModels;

public abstract class SortingAndFilteringViewModel : ViewModelBase
{
    public ObservableCollection<string> SortComboBoxList { get; set; }
    public string SortField { get; set; }

    public abstract List<string> GetComboBoxSortList();
    public abstract void Sort();
    private ActionBaseCommand _sortCommand;

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
    
    public ObservableCollection<string> FilterComboBoxList { get; set; }
    public string FilterField { get; set; }
    public string FilterTexBoxInputField { get; set; }

    public abstract List<string> GetComboBoxFilterList();
    public abstract void Filter();
    private ActionBaseCommand _filterCommand;

    public ICommand FilterCommand
    {
        get
        {
            if (_filterCommand is null)
            {
                _filterCommand = new ActionBaseCommand(Filter);
            }

            return _filterCommand;
        }
    }
}
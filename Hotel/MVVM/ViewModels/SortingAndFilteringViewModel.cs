using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Hotel.Commands;

namespace Hotel.MVVM.ViewModels;

public abstract class SortingAndFilteringViewModel<T> : ViewModelBase where T : class
{
    protected string _choosenSortField;
    protected string _filterField = string.Empty;
    private ObservableCollection<T> _items;
    private BaseCommand _sortCommand;

    public ObservableCollection<T> Items
    {
        get => _items;
        set
        {
            _items = value;
            OnPropertyChanged();
            CollectionView = CollectionViewSource.GetDefaultView(Items);
        }
    }

    // Filtering
    public ObservableCollection<string> FilterComboBoxList { get; set; }

    public string FilterField
    {
        get => _filterField;
        set
        {
            _filterField = value;
            OnPropertyChanged();
            CollectionView.Filter += Filter;
            CollectionView.Refresh();
        }
    }

    public string ChoosenFilterField { get; set; }
    public ICollectionView CollectionView { get; set; }
    protected abstract Dictionary<string, Func<T, string>> FilterByColumn { get; }

    // Sorting

    public ObservableCollection<string> SortComboBoxList { get; set; }

    public string? ChoosenSortField
    {
        get => _choosenSortField;
        set
        {
            _choosenSortField = value;
            OnPropertyChanged(ChoosenSortField);
            // SORTING RIGHT AFTER PICKING A COLUMN
            // Sort();
        }
    }

    protected ComboBoxItem _choosenDirectionSort { get; set; }

    public ComboBoxItem ChoosenDirectionSort
    {
        get => _choosenDirectionSort;
        set
        {
            _choosenDirectionSort = value;
            OnPropertyChanged();
            // SORT ON SELECTION
            // if (ChoosenSortField is not null)
            // {
            //     Sort();
            // }
        }
    }

    public ICommand SortCommand
    {
        get
        {
            if (_sortCommand is null) _sortCommand = new ActionBaseCommand(Sort);
            return _sortCommand;
        }
    }

    protected abstract List<string> LoadFilterComboBoxList();

    protected bool Filter(object obj)
    {
        if (obj is not T item) return false;

        FilterByColumn.TryGetValue(ChoosenFilterField, out var propertyAccessor);
        var propertyValue = propertyAccessor(item);
        return propertyValue?.Contains(FilterField, StringComparison.InvariantCultureIgnoreCase) ?? false;
    }

    protected List<string> LoadSortComboBoxList()
    {
        return LoadFilterComboBoxList();
    }

    protected abstract void Sort();

    protected void SortByAscOrDesc(string columnToSort)
    {
        if (columnToSort is null)
        {
            MessageBox.Show("Choose the column you want to sort on!");
            return;
        }

        CollectionView.SortDescriptions.Add(new SortDescription(columnToSort,
            (string)ChoosenDirectionSort?.Content == "Ascending" || (string)ChoosenDirectionSort?.Content == null
                ? ListSortDirection.Ascending
                : ListSortDirection.Descending));
    }

    protected void DisposeFilter()
    {
        CollectionView.Filter -= Filter;
    }
}
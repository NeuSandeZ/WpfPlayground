using System;
using System.Windows.Input;
using Hotel.Services.Interfaces;

namespace Hotel.Commands;

public class CloseModal : BaseCommand
{
    private readonly INavigator _navigator;

    public CloseModal(INavigator navigator)
    {
        _navigator = navigator;
    }

    public override void Execute(object? parameter)
    {
        _navigator.Close();
    }
}
using Hotel.Services.Interfaces;

namespace Hotel.Commands;

public class CloseModalCommand : BaseCommand
{
    private readonly INavigator _navigator;

    public CloseModalCommand(INavigator navigator)
    {
        _navigator = navigator;
    }

    public override void Execute(object? parameter)
    {
        _navigator.Close();
    }
}
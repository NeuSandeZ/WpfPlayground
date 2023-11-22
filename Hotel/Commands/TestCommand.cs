using CommunityToolkit.Mvvm.Messaging;

namespace Hotel.Commands;

public class TestCommand : BaseCommand
{
    public override void Execute(object? parameter)
    {
        WeakReferenceMessenger.Default.Send<string>("Open");
    }
}